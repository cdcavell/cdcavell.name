using CDCavell.ClassLibrary.Web.Mvc.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace cdcavell.Controllers
{
    /// <summary>
    /// Account Controller
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 10/02/2020 | Initial build |~ 
    /// </revision>
    [AllowAnonymous]
    public class AccountController : WebBaseController<AccountController>
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
        /// Entry point into the login workflow
        /// </summary>
        /// <param name="returnUrl">string</param>
        /// <returns>Task&lt;IActionResult&gt;</returns>
        /// <method>Login(string returnUrl = null)</method>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            if (!User.Identity.IsAuthenticated)
            {
                if (string.IsNullOrEmpty(returnUrl) || Url.IsLocalUrl(returnUrl))
                {
                    // Clear the existing external cookie to ensure a clean login process
                    await HttpContext.SignOutAsync();

                    var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }, Request.Scheme);
                    var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
                    properties.Items[".AuthSceme"] = "oidc";
                    return Challenge(properties, "oidc");
                }

                return RedirectToAction("Index", "Home");
            }

            return Forbid();
        }

        /// <summary>
        /// ExternalLoginCallback method
        /// </summary>
        /// <param name="ReturnUrl">string</param>
        /// <returns>IActionResult</returns>
        /// <method>ExternalLoginCallback(string ReturnUrl)</method>
        [HttpGet]
        public IActionResult ExternalLoginCallback(string ReturnUrl)
        {
            // see: Custom Requirements - This is maintained in Authorization folder
            // https://doc.microsoft.com/en-us/archive/msdn-magazine/2017/october/cutting-edge-policy-authorization-in-asp-net-core

            return Redirect(ReturnUrl);
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

        /// <summary>
        /// Handle access denied page
        /// </summary>
        /// <returns>Task&lt;IActionResult&gt;</returns>
        /// <method>AccessDenied()</method>
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
