using CDCavell.ClassLibrary.Web.Mvc.Models.Authorization;
using as_api_cdcavell.Data;
using as_api_cdcavell.Models.AppSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using System.Security.Claims;

namespace as_api_cdcavell.Controllers
{
    /// <class>IdentityController</class>
    /// <summary>
    /// User authorization controller class
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/19/2021 | Initial build Authorization Service |~ 
    /// </revision>
    public class AuthorizationController : ApplicationBaseController<AuthorizationController>
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="logger">ILogger</param>
        /// <param name="webHostEnvironment">IWebHostEnvironment</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        /// <param name="authorizationService">IAuthorizationService</param>
        /// <param name="appSettings">AppSettings</param>
        /// <param name="dbContext">CDCavellDbContext</param>
        /// <method>
        /// public AuthorizationController(
        ///     ILogger&lt;AuthorizationController&gt; logger,
        ///     IWebHostEnvironment webHostEnvironment,
        ///     IHttpContextAccessor httpContextAccessor,
        ///     IAuthorizationService authorizationService,
        ///     AppSettings appSettings,
        ///     AuthorizationServiceDbContext dbContext
        /// ) : base(logger, webHostEnvironment, httpContextAccessor, authorizationService, appSettings, dbContext)
        /// </method>
        public AuthorizationController(
            ILogger<AuthorizationController> logger,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            AppSettings appSettings,
            AuthorizationServiceDbContext dbContext
        ) : base(logger, webHostEnvironment, httpContextAccessor, authorizationService, appSettings, dbContext)
        {
        }

        /// <summary>
        /// Get action method
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            UserAuthorization userAuthorization = new UserAuthorization();
            userAuthorization.ClientId = User.Claims.Where(x => x.Type == "client_id").Select(x => x.Value).FirstOrDefault();
            userAuthorization.IdentityProvider = User.Claims.Where(x => x.Type == "http://schemas.microsoft.com/identity/claims/identityprovider").Select(x => x.Value).FirstOrDefault();
            userAuthorization.DateTimeRequsted = DateTime.Now;
            userAuthorization.Email = User.Claims.Where(x => x.Type == "email").Select(x => x.Value).FirstOrDefault();

            return new JsonResult(userAuthorization);
        }
    }
}
