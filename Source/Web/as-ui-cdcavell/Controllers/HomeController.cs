using as_ui_cdcavell.Data;
using as_ui_cdcavell.Models.AppSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;

namespace as_ui_cdcavell.Controllers
{
    /// <summary>
    /// Home controller class
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 02/02/2021 | Initial build Authorization Service |~ 
    /// | Christopher D. Cavell | 1.0.3.1 | 02/06/2021 | Utilize Redis Cache |~
    /// </revision>
    public class HomeController : ApplicationBaseController<HomeController>
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
        /// public HomeController(
        ///     ILogger&lt;HomeController&gt; logger,
        ///     IWebHostEnvironment webHostEnvironment,
        ///     IHttpContextAccessor httpContextAccessor,
        ///     IAuthorizationService authorizationService,
        ///     AppSettings appSettings,
        ///     AuthorizationUiDbContext dbContext,
        ///     IDistributedCache cache
        /// ) : base(logger, webHostEnvironment, httpContextAccessor, authorizationService, appSettings, dbContext, cache)
        /// </method>
        public HomeController(
            ILogger<HomeController> logger,
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
        /// Index method
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <method>Index()</method>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return Redirect(_appSettings.Application.MainSiteUrlTrim);
        }

        /// <summary>
        /// Privacy policy
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <method>PrivacyPolicy()</method>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult PrivacyPolicy()
        {
            return Redirect(_appSettings.Application.MainSiteUrlTrim + "/Home/PrivacyPolicy");
        }

        /// <summary>
        /// Terms of service
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <method>TermsOfService()</method>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult TermsOfService()
        {
            return Redirect(_appSettings.Application.MainSiteUrlTrim + "/Home/TermsOfService");
        }

        /// <summary>
        /// Withdraw cookie consent
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <method>WithdrawConsent()</method>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult WithdrawConsent()
        {
            return Redirect(_appSettings.Application.MainSiteUrlTrim + "/Home/WithdrawConsent");
        }

        /// <summary>
        /// Search get method
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <method>Search()</method>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Search()
        {
            return Redirect(_appSettings.Application.MainSiteUrlTrim + "/Home/Search");
        }
    }
}
