using as_ui_cdcavell.Data;
using as_ui_cdcavell.Models.AppSettings;
using as_ui_cdcavell.Models.Registration;
using CDCavell.ClassLibrary.Web.Mvc.Models.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

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
            if (!authClaim.Equals(authorization.Guid))
                return Error(400);

            UserAuthorization userAuthorization = authorization.UserAuthorization;
            if (!emailClaim.Equals(userAuthorization.Email))
                return Error(400);

            RegistrationIndexModel model = new RegistrationIndexModel();
            model.Email = userAuthorization.Email;

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
                return Error(400);
            }

            return View(model);
        }
    }
}
