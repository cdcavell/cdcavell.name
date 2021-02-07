using cdcavell.Data;
using cdcavell.Models.Account;
using cdcavell.Models.AppSettings;
using CDCavell.ClassLibrary.Web.Http;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
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
    /// | Christopher D. Cavell | 1.0.0.0 | 10/19/2020 | Initial build |~ 
    /// | Christopher D. Cavell | 1.0.0.7 | 10/31/2020 | Integrate Bing’s Adaptive URL submission API with your website [#144](https://github.com/cdcavell/cdcavell.name/issues/144) |~ 
    /// | Christopher D. Cavell | 1.0.0.9 | 11/12/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// | Christopher D. Cavell | 1.0.3.0 | 02/04/2021 | Initial build Authorization Service |~ 
    /// | Christopher D. Cavell | 1.0.3.1 | 02/06/2021 | Utilize Redis Cache |~
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
        /// <param name="cache">IDistributedCache</param>
        /// <method>
        /// AccountController(
        ///     ILogger&lt;AccountController&gt; logger, 
        ///     IWebHostEnvironment webHostEnvironment, 
        ///     IHttpContextAccessor httpContextAccessor,
        ///     IAuthorizationService authorizationService,
        ///     AppSettings appSettings,
        ///     CDCavellDbContext dbContext,
        ///     IDistributedCache cache
        /// ) : base(logger, webHostEnvironment, httpContextAccessor, authorizationService, appSettings, dbContext, cache)
        /// </method>
        public AccountController(
            ILogger<AccountController> logger,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            AppSettings appSettings,
            CDCavellDbContext dbContext,
            IDistributedCache cache
        ) : base(logger, webHostEnvironment, httpContextAccessor, authorizationService, appSettings, dbContext, cache)
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
            string url = _appSettings.Authorization.AuthorizationService.UI.TrimEnd('/').TrimEnd('\\');
            url += "/Registration/Status";
            return Redirect(url);
        }

        /// <summary>
        /// Login method
        /// </summary>
        /// <returns>Task&lt;IActionResult&gt;</returns>
        /// <method>IActionResult Login()</method>
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

                DiscoveryCache discoveryCache = (DiscoveryCache)HttpContext
                    .RequestServices.GetService(typeof(IDiscoveryCache));
                DiscoveryDocumentResponse discovery = discoveryCache.GetAsync().Result;
                if (!discovery.IsError)
                    return Redirect(discovery.EndSessionEndpoint);
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Front Channel SLO Logout method
        /// &lt;br /&gt;&lt;br /&gt;
        /// https://andersonnjen.com/2019/03/22/identityserver4-global-logout/
        /// </summary>
        /// <returns>Task&lt;IActionResult&gt;</returns>
        /// <method>FrontChannelLogout(string sid)</method>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> FrontChannelLogout(string sid)
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentSid = User.FindFirst("sid")?.Value ?? "";
                if (string.Equals(currentSid, sid, StringComparison.Ordinal))
                {
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                }
            }

            return NoContent();
        }
    }
}
