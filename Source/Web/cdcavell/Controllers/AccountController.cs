﻿using cdcavell.Data;
using cdcavell.Models.Account;
using cdcavell.Models.AppSettings;
using CDCavell.ClassLibrary.Web.Http;
using CDCavell.ClassLibrary.Web.Mvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;

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
        /// Index method
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <method>Index()</method>
        [Authorize(Policy = "Authenticated")]
        [HttpGet]
        public IActionResult Index()
        {
            UserViewModel user = (UserViewModel)ViewData["UserViewModel"];

            AccountViewModel model = new AccountViewModel();
            model.Registration = Data.Registration.Get(user.Email, _dbContext);

            if (model.Registration != null)
            {
                return View(model);
            }

            var isNewRegistration = _authorizationService.AuthorizeAsync(User, "NewRegistration").Result;
            if (isNewRegistration.Succeeded)
                return RedirectToAction("Registration", "Account");

            return RedirectToAction("Logout", "Account");
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
        /// Registration HttpGet method
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <method>Registration()</method>
        [Authorize(Policy = "NewRegistration")]
        [HttpGet]
        public IActionResult Registration()
        {
            UserViewModel user = (UserViewModel)ViewData["UserViewModel"];

            Registration registration = Data.Registration.Get(user.Email, _dbContext);
            if (registration == null)
            {
                AccountViewModel model = new AccountViewModel();
                model.Registration = new Registration();
                model.Registration.Email = user.Email;

                return View(model);
            }

            if (registration.IsPending)
                return RedirectToAction("Index", "Account");

            return RedirectToAction("Logout", "Account");
        }

        /// <summary>
        /// Registration HttpPost method
        /// </summary>
        /// <param name="model">AccountViewModel</param>
        /// <returns>IActionResult</returns>
        /// <method>Registration(AccountViewModel model)</method>
        [Authorize(Policy = "NewRegistration")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(AccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Registration.Email = model.Registration.Email.Trim().Clean();
                model.Registration.FirstName = model.Registration.FirstName.Trim().Clean();
                model.Registration.LastName = model.Registration.LastName.Trim().Clean();
                model.Registration.RequestDate = DateTime.Now;
                model.Registration.AddUpdate(_dbContext);

                return RedirectToAction("Index", "Account");
            }

            return View(model);
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
        public IActionResult Delete(AccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Registration.Delete(_dbContext);
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
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return SignOut(CookieAuthenticationDefaults.AuthenticationScheme, "oidc");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
