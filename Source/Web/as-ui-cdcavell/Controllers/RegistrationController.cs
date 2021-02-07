using as_ui_cdcavell.Data;
using as_ui_cdcavell.Models.AppSettings;
using as_ui_cdcavell.Models.Registration;
using CDCavell.ClassLibrary.Web.Http;
using CDCavell.ClassLibrary.Web.Mvc.Models.Authorization;
using CDCavell.ClassLibrary.Web.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;

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
    /// | Christopher D. Cavell | 1.0.3.1 | 02/06/2021 | Utilize Redis Cache |~
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
        /// <param name="cache">IDistributedCache</param>
        /// <method>
        /// public RegistrationController(
        ///     ILogger&lt;HomeController&gt; logger,
        ///     IWebHostEnvironment webHostEnvironment,
        ///     IHttpContextAccessor httpContextAccessor,
        ///     IAuthorizationService authorizationService,
        ///     AppSettings appSettings,
        ///     AuthorizationUiDbContext dbContext,
        ///     IDistributedCache cache
        /// ) : base(logger, webHostEnvironment, httpContextAccessor, authorizationService, appSettings, dbContext, cache)
        /// </method>
        public RegistrationController(
            ILogger<RegistrationController> logger,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            AppSettings appSettings,
            AuthorizationUiDbContext dbContext,
            IDistributedCache cache
        ) : base(logger, webHostEnvironment, httpContextAccessor, authorizationService, appSettings, dbContext, cache)
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

            UserAuthorization userAuthorization = HttpContext.Session.Decrypt<UserAuthorization>("UserAuthorization").Result;
            if (userAuthorization != null)
                if (userAuthorization.Registration.IsRegistered)
                    return RedirectToAction("Status", "Registration");

            RegistrationIndexModel model = new RegistrationIndexModel();
            model.Email = emailClaim;

            return View(model);
        }

        /// <summary>
        /// New Registration HttpPost method
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <method>Index(RegistrationIndexModel model)</method>
        [Authorize(Policy = "Authenticated")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(RegistrationIndexModel model)
        {
            if (ModelState.IsValid)
            {
                string emailClaim = User.Claims.Where(x => x.Type == "email").Select(x => x.Value).FirstOrDefault();
                if (string.IsNullOrEmpty(emailClaim))
                    return Error(400);

                string accessToken = HttpContext.Session.Decrypt<string>("AccessToken").Result;
                if (string.IsNullOrEmpty(accessToken))
                    return Error(400);

                UserAuthorization userAuthorization = HttpContext.Session.Decrypt<UserAuthorization>("UserAuthorization").Result;
                if (userAuthorization != null)
                    if (userAuthorization.Registration.IsRegistered)
                        return RedirectToAction("Status", "Registration");

                userAuthorization = new UserAuthorization();
                userAuthorization.Registration.Email = model.Email.Clean();
                userAuthorization.Registration.FirstName = model.FirstName.Clean();
                userAuthorization.Registration.LastName = model.LastName.Clean();

                string jsonString = JsonConvert.SerializeObject(userAuthorization);
                string encryptString = AESGCM.Encrypt(jsonString, accessToken);

                JsonClient jsonClient = new JsonClient(_appSettings.Authorization.AuthorizationService.API, accessToken);
                HttpStatusCode statusCode = jsonClient.SendRequest(HttpMethod.Put, "Registration", encryptString);
                if (!jsonClient.IsResponseSuccess)
                {
                    _logger.Exception(new Exception(jsonClient.GetResponseString()));
                    return Error(7004);
                }

                jsonString = AESGCM.Decrypt(jsonClient.GetResponseObject<string>(), accessToken);
                userAuthorization = JsonConvert.DeserializeObject<UserAuthorization>(jsonString);

                HttpContext.Session.Encrypt<UserAuthorization>("UserAuthorization", userAuthorization);

                return RedirectToAction("Status", "Registration");
            }

            return View(model);
        }

        /// <summary>
        /// Registration Status HttpGet method
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <method>Index()</method>
        [Authorize(Policy = "Authenticated")]
        [HttpGet]
        public IActionResult Status()
        {
            string emailClaim = User.Claims.Where(x => x.Type == "email").Select(x => x.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(emailClaim))
                return Error(400);

            UserAuthorization userAuthorization = HttpContext.Session.Decrypt<UserAuthorization>("UserAuthorization").Result;
            if (userAuthorization != null)
                if (!userAuthorization.Registration.IsRegistered)
                    return RedirectToAction("Index", "Registration");

            if (!emailClaim.Equals(userAuthorization.Email))
                return Error(400);

            RegistrationIndexModel model = new RegistrationIndexModel();
            model.Email = userAuthorization.Registration.Email;
            model.FirstName = userAuthorization.Registration.FirstName;
            model.LastName = userAuthorization.Registration.LastName;
            model.RequestDate = userAuthorization.Registration.RequestDate;
            model.Status = userAuthorization.Registration.Status;

            return View(model);
        }

        /// <summary>
        /// Delete Account
        /// </summary>
        /// <param name="model">RegistrationIndexModel</param>
        /// <returns>IActionResult</returns>
        /// <method>Delete(RegistrationIndexModel model)</method>
        [Authorize(Policy = "Authenticated")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(RegistrationIndexModel model)
        {
            if (ModelState.IsValid)
            {
                string emailClaim = User.Claims.Where(x => x.Type == "email").Select(x => x.Value).FirstOrDefault();
                if (string.IsNullOrEmpty(emailClaim))
                    return Error(400);

                if (!emailClaim.Equals(model.Email))
                    return Error(400);

                string accessToken = HttpContext.Session.Decrypt<string>("AccessToken").Result;
                if (string.IsNullOrEmpty(accessToken))
                    return Error(400);

                UserAuthorization userAuthorization = HttpContext.Session.Decrypt<UserAuthorization>("UserAuthorization").Result;
                if (userAuthorization != null)
                    if (userAuthorization.Registration.IsRegistered)
                    {
                        string jsonString = JsonConvert.SerializeObject(userAuthorization);
                        string encryptString = AESGCM.Encrypt(jsonString, accessToken);

                        JsonClient jsonClient = new JsonClient(_appSettings.Authorization.AuthorizationService.API, accessToken);
                        HttpStatusCode statusCode = jsonClient.SendRequest(HttpMethod.Delete, "Registration", encryptString);
                        if (!jsonClient.IsResponseSuccess)
                        {
                            _logger.Exception(new Exception(jsonClient.GetResponseString()));
                            return Error(7005);
                        }
                    }

                return RedirectToAction("Logout", "Account");
            }

            return View(model);
        }
    }
}
