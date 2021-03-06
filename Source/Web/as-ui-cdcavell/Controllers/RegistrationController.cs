using as_ui_cdcavell.Data;
using as_ui_cdcavell.Models.AppSettings;
using as_ui_cdcavell.Models.Registration;
using CDCavell.ClassLibrary.Web.Http;
using CDCavell.ClassLibrary.Web.Security;
using CDCavell.ClassLibrary.Web.Services.Authorization;
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
using System.Threading.Tasks;

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
    /// | Christopher D. Cavell | 1.0.3.3 | 03/06/2021 | User Authorization Web Service |~ 
    /// </revision>
    public class RegistrationController : ApplicationBaseController<RegistrationController>
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="logger">ILogger</param>
        /// <param name="webHostEnvironment">IWebHostEnvironment</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        /// <param name="authorizationService">IAuthorizationService</param>
        /// <param name="appSettings">AppSettings</param>
        /// <param name="dbContext">AuthorizationUiDbContext</param>
        /// <method>
        /// public RegistrationController(
        ///     ILogger&lt;HomeController&gt; logger,
        ///     IWebHostEnvironment webHostEnvironment,
        ///     IHttpContextAccessor httpContextAccessor,
        ///     IAuthorizationService authorizationService,
        ///     AppSettings appSettings,
        ///     AuthorizationUiDbContext dbContext
        /// ) : base(logger, webHostEnvironment, httpContextAccessor, authorizationService, appSettings, dbContext)
        /// </method>
        public RegistrationController(
            ILogger<RegistrationController> logger,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            AppSettings appSettings,
            AuthorizationUiDbContext dbContext
        ) : base(logger, webHostEnvironment, httpContextAccessor, authorizationService, appSettings, dbContext)
        {
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

            Data.Authorization authorization = Data.Authorization.GetRecord(User.Claims, _dbContext);
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

                Data.Authorization authorization = Data.Authorization.GetRecord(User.Claims, _dbContext);
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

                return RedirectToAction("Status", "Registration");
            }

            return View(model);
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

            Data.Authorization authorization = Data.Authorization.GetRecord(User.Claims, _dbContext);
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

            if (userAuthorization.Registration.IsActive)
                model.RolePermissions = userAuthorization.RolePermissions
                    .Where(x => x.Status.StatusId == 2)
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

                Data.Authorization authorization = Data.Authorization.GetRecord(User.Claims, _dbContext);
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

            Data.Authorization authorization = Data.Authorization.GetRecord(User.Claims, _dbContext);
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

                Data.Authorization authorization = Data.Authorization.GetRecord(User.Claims, _dbContext);
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
