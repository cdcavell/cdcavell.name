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
    /// | Christopher D. Cavell | 1.0.3.0 | 01/31/2021 | Initial build Authorization Service |~ 
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
            if (!authorization.UserAuthorization.RegistrationStatus.Equals("Not Registered", StringComparison.OrdinalIgnoreCase))
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

                string authClaim = User.Claims.Where(x => x.Type == "authorization").Select(x => x.Value).FirstOrDefault();
                if (string.IsNullOrEmpty(authClaim))
                    return Error(400);

                Data.Authorization authorization = Data.Authorization.GetRecord(User.Claims, _dbContext);
                if (!authorization.UserAuthorization.RegistrationStatus.Equals("Not Registered", StringComparison.OrdinalIgnoreCase))
                    return RedirectToAction("Status", "Registration");

                UserAuthorization userAuthorization = new UserAuthorization();
                userAuthorization.Email = model.Email.Clean();
                userAuthorization.FirstName = model.FirstName.Clean();
                userAuthorization.LastName = model.LastName.Clean();

                JsonClient jsonClient = new JsonClient(_appSettings.Authorization.AuthorizationService.API, authorization.AccessToken);
                HttpStatusCode statusCode = jsonClient.SendRequest(HttpMethod.Put, "Registration", userAuthorization);
                if (!jsonClient.IsResponseSuccess)
                {
                    _logger.Exception(new Exception(jsonClient.GetResponseString()));
                    return Error(7004);
                }

                string jsonString = AESGCM.Decrypt(jsonClient.GetResponseObject<string>(), authorization.AccessToken);
                userAuthorization = JsonConvert.DeserializeObject<UserAuthorization>(jsonString);

                authorization.UserAuthorization = userAuthorization;
                authorization.AddUpdate(_dbContext);

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

            string authClaim = User.Claims.Where(x => x.Type == "authorization").Select(x => x.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(authClaim))
                return Error(400);

            Data.Authorization authorization = Data.Authorization.GetRecord(User.Claims, _dbContext);
            if (!authClaim.Equals(authorization.Guid))
                return Error(400);

            UserAuthorization userAuthorization = authorization.UserAuthorization;
            if (userAuthorization.RegistrationStatus.Equals("Not Registered", StringComparison.OrdinalIgnoreCase))
                return RedirectToAction("Index", "Registration");

            if (!emailClaim.Equals(userAuthorization.Email))
                return Error(400);

            RegistrationIndexModel model = new RegistrationIndexModel();
            model.Email = userAuthorization.Email;
            model.FirstName = userAuthorization.FirstName;
            model.LastName = userAuthorization.LastName;
            model.Status = userAuthorization.RegistrationStatus;

            return View(model);
        }
    }
}
