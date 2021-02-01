using as_ui_cdcavell.Data;
using as_ui_cdcavell.Models.Account;
using as_ui_cdcavell.Models.AppSettings;
using CDCavell.ClassLibrary.Web.Http;
using CDCavell.ClassLibrary.Web.Mvc.Models.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;

namespace as_ui_cdcavell.Controllers
{
    /// <summary>
    /// Account Controller
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 02/01/2021 | Initial build Authorization Service |~ 
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
        /// <param name="dbContext">AuthorizationUiDbContext</param>
        /// <method>
        /// AccountController(
        ///     ILogger&lt;AccountController&gt; logger, 
        ///     IWebHostEnvironment webHostEnvironment, 
        ///     IHttpContextAccessor httpContextAccessor,
        ///     IAuthorizationService authorizationService,
        ///     AppSettings appSettings,
        ///     AuthorizationUiDbContext dbContext
        /// ) : base(logger, webHostEnvironment, httpContextAccessor, appSettings, dbContext)
        /// </method>
        public AccountController(
            ILogger<AccountController> logger,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            AppSettings appSettings,
            AuthorizationUiDbContext dbContext
        ) : base(logger, webHostEnvironment, httpContextAccessor, authorizationService, appSettings, dbContext)
        {
        }

        /// <summary>
        /// Index method
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <method>Index()</method>
        [Authorize(Policy = "Authenticated")]
        [HttpGet]
        public IActionResult Index()
        {
            UserAuthorization userAuthorization = Data.Authorization.GetUser(User.Claims, _dbContext);
            return View(userAuthorization);
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
            return RedirectToAction("Index", "Account");
        }

        /// <summary>
        /// Validate returned captcha token
        /// </summary>
        /// <param name="captchaToken">string</param>
        /// <returns>IActionResult</returns>
        /// <method>ValidateCaptchaToken(string captchaToken)</method>
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ValidateCaptchaToken(string captchaToken)
        {
            if (ModelState.IsValid)
            {
                string request = "siteverify";
                request += "?secret=" + _appSettings.Authentication.reCAPTCHA.SecretKey;
                request += "&response=" + captchaToken.Trim().Clean();
                request += "&remoteip=" + _httpContextAccessor.HttpContext.GetRemoteAddress().MapToIPv4().ToString();

                JsonClient client = new JsonClient(" https://www.google.com/recaptcha/api/");
                HttpStatusCode statusCode = client.SendRequest(HttpMethod.Post, request, string.Empty);
                if (client.IsResponseSuccess)
                {
                    CaptchaResponse response = client.GetResponseObject<CaptchaResponse>();
                    if (response.success)
                        if (response.action.Equals("submit", StringComparison.OrdinalIgnoreCase))
                            if (response.score > 0.6)
                                return Ok(client.GetResponseString());
                }

                return BadRequest(client.GetResponseString());
            }

            return BadRequest("Invalid request");
        }

        /// <summary>
        /// Delete Account
        /// </summary>
        /// <param name="model">AccountViewModel</param>
        /// <returns>IActionResult</returns>
        /// <method>Delete(AccountViewModel model)</method>
        [Authorize(Policy = "Authenticated")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(UserAuthorization model)
        {
            if (ModelState.IsValid)
            {
                //TODO: Call Authorization Service API - Delete Registration

                Claim authorizationClaim = User.Claims.Where(x => x.Type == "authorization").FirstOrDefault();
                if (authorizationClaim != null)
                {
                    Data.Authorization authorization = _dbContext.Authorization
                    .Where(x => x.Guid == authorizationClaim.Value.ToString())
                    .FirstOrDefault();

                    authorization.Delete(_dbContext);
                }

                return RedirectToAction("Logout", "Account");
            }

            return View(model);
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
                // Remove Authorization record
                Data.Authorization authorization = Data.Authorization.GetRecord(User.Claims, _dbContext);
                authorization.Delete(_dbContext);

                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
                SignOut(CookieAuthenticationDefaults.AuthenticationScheme, "oidc");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
