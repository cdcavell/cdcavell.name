using as_ui_cdcavell.Models.AppSettings;
using as_ui_cdcavell.Models.Registration;
using CDCavell.ClassLibrary.Commons;
using CDCavell.ClassLibrary.Web.Http;
using CDCavell.ClassLibrary.Web.Security;
using CDCavell.ClassLibrary.Web.Services.Authorization;
using CDCavell.ClassLibrary.Web.Services.Data;
using CDCavell.ClassLibrary.Web.Services.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace as_ui_cdcavell.Controllers
{
    /// <summary>
    /// Registration controller class
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 02/01/2021 | Initial build Authorization Service |~ 
    /// | Christopher D. Cavell | 1.0.3.1 | 02/08/2021 | User Authorization Web Service |~ 
    /// | Christopher D. Cavell | 1.0.3.3 | 03/13/2021 | User Authorization Web Service |~ 
    /// </revision>
    public class RegistrationController : ApplicationBaseController<RegistrationController>
    {
        private IEmailService _emailService;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="logger">ILogger</param>
        /// <param name="webHostEnvironment">IWebHostEnvironment</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        /// <param name="authorizationService">IAuthorizationService</param>
        /// <param name="appSettings">AppSettings</param>
        /// <param name="dbContext">AuthorizationDbContext</param>
        /// <param name="emailService">EmailService</param>
        /// <method>
        /// public RegistrationController(
        ///     ILogger&lt;HomeController&gt; logger,
        ///     IWebHostEnvironment webHostEnvironment,
        ///     IHttpContextAccessor httpContextAccessor,
        ///     IAuthorizationService authorizationService,
        ///     AppSettings appSettings,
        ///     AuthorizationDbContext dbContext,
        ///     IEmailService emailService
        /// ) : base(logger, webHostEnvironment, httpContextAccessor, authorizationService, appSettings, dbContext)
        /// </method>
        public RegistrationController(
            ILogger<RegistrationController> logger,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            AppSettings appSettings,
            AuthorizationDbContext dbContext,
            IEmailService emailService
        ) : base(logger, webHostEnvironment, httpContextAccessor, authorizationService, appSettings, dbContext)
        {
            _emailService = emailService;
        }

        /// <summary>
        /// New Registration HttpGet method
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <method>Index()</method>
        [Authorize(Policy = "Authenticated")]
        [HttpGet]
        public IActionResult Index()
        {
            string emailClaim = User.Claims.Where(x => x.Type == "email").Select(x => x.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(emailClaim))
                return Error(400);

            string authClaim = User.Claims.Where(x => x.Type == "authorization").Select(x => x.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(authClaim))
                return Error(400);

            CDCavell.ClassLibrary.Web.Services.Data.Authorization authorization = CDCavell.ClassLibrary.Web.Services.Data.Authorization.GetRecord(User.Claims, _dbContext);
            if (authorization.UserAuthorization.Registration.IsActive
             || authorization.UserAuthorization.Registration.IsPending)
                return RedirectToAction("Status", "Registration");

            // Only allow self revoked to register again
            if (authorization.UserAuthorization.Registration.IsRevoked)
                if (!authorization.UserAuthorization.Registration.Email
                    .Equals(authorization.UserAuthorization.Registration.RevokedBy))
                    return RedirectToAction("Status", "Registration");

            RegistrationIndexModel model = new RegistrationIndexModel();
            model.Email = emailClaim;
            model.FirstName = authorization.UserAuthorization.Registration.FirstName ?? string.Empty;
            model.LastName = authorization.UserAuthorization.Registration.LastName ?? string.Empty;

            return View(model);
        }

        /// <summary>
        /// New Registration HttpPost method
        /// </summary>
        /// <returns>Task&lt;IActionResult&gt;</returns>
        /// <method>Index(RegistrationIndexModel model)</method>
        [Authorize(Policy = "Authenticated")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(RegistrationIndexModel model)
        {
            if (ModelState.IsValid)
            {
                string emailClaim = User.Claims.Where(x => x.Type == "email").Select(x => x.Value).FirstOrDefault();
                if (string.IsNullOrEmpty(emailClaim))
                    return Error(400);

                string authClaim = User.Claims.Where(x => x.Type == "authorization").Select(x => x.Value).FirstOrDefault();
                if (string.IsNullOrEmpty(authClaim))
                    return Error(400);

                CDCavell.ClassLibrary.Web.Services.Data.Authorization authorization = CDCavell.ClassLibrary.Web.Services.Data.Authorization.GetRecord(User.Claims, _dbContext);
                if (authorization.UserAuthorization.Registration.IsActive
                 || authorization.UserAuthorization.Registration.IsPending)
                    return RedirectToAction("Status", "Registration");

                // Only allow self revoked to register again
                if (authorization.UserAuthorization.Registration.IsRevoked)
                    if (!authorization.UserAuthorization.Registration.Email
                        .Equals(authorization.UserAuthorization.Registration.RevokedBy))
                        return RedirectToAction("Status", "Registration");

                UserAuthorizationModel userAuthorization = new UserAuthorizationModel();
                userAuthorization.Registration.Email = model.Email.Clean();
                userAuthorization.Registration.FirstName = model.FirstName.Clean();
                userAuthorization.Registration.LastName = model.LastName.Clean();

                string jsonString = JsonConvert.SerializeObject(userAuthorization);
                string encryptString = AESGCM.Encrypt(jsonString, authorization.AccessToken);

                JsonClient jsonClient = new JsonClient(_appSettings.Authorization.AuthorizationService.API, authorization.AccessToken);
                HttpStatusCode statusCode = await jsonClient.SendRequest(HttpMethod.Put, "Registration", encryptString);
                if (!jsonClient.IsResponseSuccess)
                {
                    _logger.Exception(new Exception(jsonClient.GetResponseString()));
                    return Error(400);
                }

                jsonString = AESGCM.Decrypt(jsonClient.GetResponseObject<string>(), authorization.AccessToken);
                userAuthorization = JsonConvert.DeserializeObject<UserAuthorizationModel>(jsonString);

                authorization.UserAuthorization = userAuthorization;
                authorization.AddUpdate(_dbContext);

                return RedirectToAction("EmailValidation", "Registration");
            }

            return View(model);
        }

        /// <summary>
        /// Send email verification HttpGet method
        /// </summary>
        /// <returns>Task&lt;IActionResult&gt;</returns>
        /// <method>EmailValidation()</method>
        [Authorize(Policy = "Authenticated")]
        [HttpGet]
        public async Task<IActionResult> EmailValidation()
        {
            string emailClaim = User.Claims.Where(x => x.Type == "email").Select(x => x.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(emailClaim))
                return Error(400);

            string authClaim = User.Claims.Where(x => x.Type == "authorization").Select(x => x.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(authClaim))
                return Error(400);

            CDCavell.ClassLibrary.Web.Services.Data.Authorization authorization = CDCavell.ClassLibrary.Web.Services.Data.Authorization.GetRecord(User.Claims, _dbContext);
            if (authorization.UserAuthorization.Registration.IsRegistered)
                if (authorization.UserAuthorization.Registration.PendingValidation)
                {
                    MailMessage mailMessage = new MailMessage(
                        _appSettings.EmailService.Email,
                        authorization.UserAuthorization.Email
                        );

                    string validateString =
                          authorization.UserAuthorization.Registration.RegistrationId.ToString()
                        + ";"
                        + authorization.UserAuthorization.Registration.Email
                        + ";"
                        + authorization.UserAuthorization.Registration.ValidationToken;
                    string encryptString = AESGCM.Encrypt(validateString);
                    byte[] byteArray = UTF8Encoding.UTF8.GetBytes(encryptString);

                    string url = _appSettings.Authorization.AuthorizationService.UiTrim
                        + "/Registration/EmailValidate?Request="
                        + HttpUtility.HtmlEncode(Convert.ToBase64String(byteArray));

                    mailMessage.Subject = "Email Validation";
                    mailMessage.IsBodyHtml = false;
                    mailMessage.Body = AsciiCodes.CRLF
                        + "Please submit following link in your web browser for email validation:"
                        + AsciiCodes.CRLF + url;
                        
                    await _emailService.Send(mailMessage);
                }

            return RedirectToAction("Status", "Registration");
        }

        /// <summary>
        /// Send email verification HttpGet method
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <method>EmailValidation(string Request)</method>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult EmailValidate(string Request)
        {
            try
            {
                byte[] byteArray = Convert.FromBase64String(HttpUtility.HtmlDecode(Request.Clean()));
                string encryptString = UTF8Encoding.UTF8.GetString(byteArray);
                string decryptString = AESGCM.Decrypt(encryptString);

                string[] items = decryptString.Split(';');
                long registrationId = long.Parse(items[0].ToString());
                string email = items[1].ToString();
                string guid = items[2].ToString();

               return RedirectToAction("Status", "Registration");
            }
            catch (Exception exception)
            {
                _logger.Exception(exception);
            }

            return BadRequest();
        }

        /// <summary>
        /// Registration Status HttpGet method
        /// </summary>
        /// <returns>Task&lt;IActionResult&gt;</returns>
        /// <method>Index()</method>
        [Authorize(Policy = "Authenticated")]
        [HttpGet]
        public async Task<IActionResult> Status()
        {
            string emailClaim = User.Claims.Where(x => x.Type == "email").Select(x => x.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(emailClaim))
                return Error(400);

            string authClaim = User.Claims.Where(x => x.Type == "authorization").Select(x => x.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(authClaim))
                return Error(400);

            CDCavell.ClassLibrary.Web.Services.Data.Authorization authorization = CDCavell.ClassLibrary.Web.Services.Data.Authorization.GetRecord(User.Claims, _dbContext);
            if (!authClaim.Equals(authorization.Guid))
                return Error(400);

            UserAuthorizationModel userAuthorization = authorization.UserAuthorization;
            if (!userAuthorization.Registration.IsRegistered)
                return RedirectToAction("Index", "Registration");

            if (!emailClaim.Equals(userAuthorization.Email))
                return Error(400);

            // Allow self revoked to register again
            if (userAuthorization.Registration.IsRevoked)
                if (userAuthorization.Registration.Email == userAuthorization.Registration.RevokedBy)
                    return RedirectToAction("Index", "Registration");

            string jsonString = JsonConvert.SerializeObject(userAuthorization);
            string encryptString = AESGCM.Encrypt(jsonString, authorization.AccessToken);

            JsonClient jsonClient = new JsonClient(_appSettings.Authorization.AuthorizationService.API, authorization.AccessToken);
            HttpStatusCode statusCode = await jsonClient.SendRequest(HttpMethod.Get, "AllPermissions", encryptString);
            if (!jsonClient.IsResponseSuccess)
            {
                _logger.Exception(new Exception(jsonClient.GetResponseString()));
                return Error(400);
            }

            jsonString = AESGCM.Decrypt(jsonClient.GetResponseObject<string>(), authorization.AccessToken);
            userAuthorization = JsonConvert.DeserializeObject<UserAuthorizationModel>(jsonString);

            RegistrationIndexModel model = new RegistrationIndexModel();
            model.Email = userAuthorization.Registration.Email;
            model.FirstName = userAuthorization.Registration.FirstName;
            model.LastName = userAuthorization.Registration.LastName;
            model.RequestDate = userAuthorization.Registration.RequestDate;
            model.Status = userAuthorization.Registration.Status;
            model.PendingValidation = userAuthorization.Registration.PendingValidation;

            if (userAuthorization.Registration.IsActive)
                model.RolePermissions = userAuthorization.RolePermissions
                    .OrderBy(x => x.Role.Resource.Description)
                    .OrderBy(x => x.Role.Description)
                    .OrderBy(x => x.Permission.Description)
                    .ToList();

            return View(model);
        }

        /// <summary>
        /// Delete Account
        /// </summary>
        /// <param name="model">RegistrationIndexModel</param>
        /// <returns>Task&lt;IActionResult&gt;</returns>
        /// <method>Delete(RegistrationIndexModel model)</method>
        [Authorize(Policy = "Authenticated")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(RegistrationIndexModel model)
        {
            if (ModelState.IsValid)
            {
                string emailClaim = User.Claims.Where(x => x.Type == "email").Select(x => x.Value).FirstOrDefault();
                if (string.IsNullOrEmpty(emailClaim))
                    return Error(400);

                if (!emailClaim.Equals(model.Email))
                    return Error(400);

                string authClaim = User.Claims.Where(x => x.Type == "authorization").Select(x => x.Value).FirstOrDefault();
                if (string.IsNullOrEmpty(authClaim))
                    return Error(400);

                CDCavell.ClassLibrary.Web.Services.Data.Authorization authorization = CDCavell.ClassLibrary.Web.Services.Data.Authorization.GetRecord(User.Claims, _dbContext);
                if (authorization.UserAuthorization.Registration.IsRegistered)
                {
                    UserAuthorizationModel userAuthorization = authorization.UserAuthorization;
                    string jsonString = JsonConvert.SerializeObject(userAuthorization);
                    string encryptString = AESGCM.Encrypt(jsonString, authorization.AccessToken);

                    JsonClient jsonClient = new JsonClient(_appSettings.Authorization.AuthorizationService.API, authorization.AccessToken);
                    HttpStatusCode statusCode = await jsonClient.SendRequest(HttpMethod.Delete, "Registration", encryptString);
                    if (!jsonClient.IsResponseSuccess)
                    {
                        _logger.Exception(new Exception(jsonClient.GetResponseString()));
                        return Error(500);
                    }
                }

                return RedirectToAction("Logout", "Account");
            }

            return View(model);
        }

        /// <summary>
        /// Update Registration HttpGet method
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <method>Update()</method>
        [Authorize(Policy = "Authenticated")]
        [HttpGet]
        public IActionResult Update()
        {
            string emailClaim = User.Claims.Where(x => x.Type == "email").Select(x => x.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(emailClaim))
                return Error(400);

            string authClaim = User.Claims.Where(x => x.Type == "authorization").Select(x => x.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(authClaim))
                return Error(400);

            CDCavell.ClassLibrary.Web.Services.Data.Authorization authorization = CDCavell.ClassLibrary.Web.Services.Data.Authorization.GetRecord(User.Claims, _dbContext);
            if (!authorization.UserAuthorization.Registration.IsActive
             && !authorization.UserAuthorization.Registration.IsPending)
                return RedirectToAction("Status", "Registration");

            RegistrationIndexModel model = new RegistrationIndexModel();
            model.Email = emailClaim;
            model.FirstName = authorization.UserAuthorization.Registration.FirstName ?? string.Empty;
            model.LastName = authorization.UserAuthorization.Registration.LastName ?? string.Empty;

            return View(model);
        }

        /// <summary>
        /// Update Registration HttpPost method
        /// </summary>
        /// <returns>Task&lt;IActionResult&gt;</returns>
        /// <method>Update(RegistrationIndexModel model)</method>
        [Authorize(Policy = "Authenticated")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(RegistrationIndexModel model)
        {
            if (ModelState.IsValid)
            {
                string emailClaim = User.Claims.Where(x => x.Type == "email").Select(x => x.Value).FirstOrDefault();
                if (string.IsNullOrEmpty(emailClaim))
                    return Error(400);

                string authClaim = User.Claims.Where(x => x.Type == "authorization").Select(x => x.Value).FirstOrDefault();
                if (string.IsNullOrEmpty(authClaim))
                    return Error(400);

                CDCavell.ClassLibrary.Web.Services.Data.Authorization authorization = CDCavell.ClassLibrary.Web.Services.Data.Authorization.GetRecord(User.Claims, _dbContext);
                if (!authorization.UserAuthorization.Registration.IsActive
                 && !authorization.UserAuthorization.Registration.IsPending)
                    return RedirectToAction("Status", "Registration");

                UserAuthorizationModel userAuthorization = new UserAuthorizationModel();
                userAuthorization.Registration.Email = model.Email.Clean();
                userAuthorization.Registration.FirstName = model.FirstName.Clean();
                userAuthorization.Registration.LastName = model.LastName.Clean();

                string jsonString = JsonConvert.SerializeObject(userAuthorization);
                string encryptString = AESGCM.Encrypt(jsonString, authorization.AccessToken);

                JsonClient jsonClient = new JsonClient(_appSettings.Authorization.AuthorizationService.API, authorization.AccessToken);
                HttpStatusCode statusCode = await jsonClient.SendRequest(HttpMethod.Put, "Registration", encryptString);
                if (!jsonClient.IsResponseSuccess)
                {
                    _logger.Exception(new Exception(jsonClient.GetResponseString()));
                    return Error(400);
                }

                jsonString = AESGCM.Decrypt(jsonClient.GetResponseObject<string>(), authorization.AccessToken);
                userAuthorization = JsonConvert.DeserializeObject<UserAuthorizationModel>(jsonString);

                authorization.UserAuthorization = userAuthorization;
                authorization.AddUpdate(_dbContext);

                return RedirectToAction("Status", "Registration");
            }

            return View(model);
        }
    }
}
