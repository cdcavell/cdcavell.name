using CDCavell.ClassLibrary.Web.Mvc.Controllers;
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
    /// | Christopher D. Cavell | 1.0.0 | 10/12/2020 | Initial build |~ 
    /// </revision>
    [AllowAnonymous]
    public class AccountController : ApplicationBaseController<AccountController>
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="logger">ILogger&lt;AccountController&gt;</param>
        /// <param name="webHostEnvironment">IWebHostEnvironment</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        /// <method>
        /// AccountController(
        ///     ILogger&lt;AccountController&gt; logger, 
        ///     IWebHostEnvironment webHostEnvironment, 
        ///     IHttpContextAccessor httpContextAccessor
        /// ) : base(logger, webHostEnvironment, httpContextAccessor)
        /// </method>
        public AccountController(
            ILogger<AccountController> logger,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor
        ) : base(logger, webHostEnvironment, httpContextAccessor)
        {
        }

        /// <summary>
        /// Logout method
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <method>Logout()</method>
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
