using System;
using cdcavell.Data;
using cdcavell.Models.AppSettings;
using CDCavell.ClassLibrary.Web.Mvc.Controllers;
using CDCavell.ClassLibrary.Web.Mvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace cdcavell.Controllers
{
    /// <summary>
    /// Account Controller
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 10/19/2020 | Initial build |~ 
    /// | Christopher D. Cavell | 1.0.0.7 | 10/31/2020 | Integrate Bing’s Adaptive URL submission API with your website [#144](https://github.com/cdcavell/cdcavell.name/issues/144) |~ 
    /// | Christopher D. Cavell | 1.0.0.9 | 11/04/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// </revision>
    public class AccountController : ApplicationBaseController<AccountController>
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="logger">ILogger&lt;AccountController&gt;</param>
        /// <param name="webHostEnvironment">IWebHostEnvironment</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        /// <param name="authorizationService">IAuthorizationService</param>
        /// <param name="appSettings">AppSettings</param>
        /// <param name="dbContext">CDCavellDbContext</param>
        /// <method>
        /// AccountController(
        ///     ILogger&lt;AccountController&gt; logger, 
        ///     IWebHostEnvironment webHostEnvironment, 
        ///     IHttpContextAccessor httpContextAccessor,
        ///     IAuthorizationService authorizationService,
        ///     AppSettings appSettings,
        ///     CDCavellDbContext dbContext
        /// ) : base(logger, webHostEnvironment, httpContextAccessor, appSettings, dbContext)
        /// </method>
        public AccountController(
            ILogger<AccountController> logger,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            AppSettings appSettings,
            CDCavellDbContext dbContext
        ) : base(logger, webHostEnvironment, httpContextAccessor, authorizationService, appSettings, dbContext)
        {
        }

        /// <summary>
        /// Login method
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <method>Login()</method>
        [Authorize(Policy = "Authenticated")]
        [HttpGet]
        public IActionResult Login()
        {
            var isNewRegistration = _authorizationService.AuthorizeAsync(User, "NewRegistration").Result;
            if (isNewRegistration.Succeeded)
            {
                return RedirectToAction("Registration", "Account");
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Registration method
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <method>Registration()</method>
        [Authorize(Policy = "NewRegistration")]
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        /// <summary>
        /// Logout method
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <method>Logout()</method>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return SignOut(CookieAuthenticationDefaults.AuthenticationScheme, "oidc");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
