﻿using CDCavell.ClassLibrary.Web.Mvc.Controllers;
using CDCavell.ClassLibrary.Web.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace cdcavell.Controllers
{
    /// <summary>
    /// Home controller class
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 10/02/2020 | Initial build |~ 
    /// </revision>
    public class HomeController : WebBaseController<HomeController>
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="logger">ILogger</param>
        /// <param name="webHostEnvironment">IWebHostEnvironment</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        /// <method>HomeController(ILogger&lt;HomeController&gt; logger, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)</method>
        public HomeController(
            ILogger<HomeController> logger,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor
        ) : base(logger, webHostEnvironment, httpContextAccessor)
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
            return View();
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
            return View();
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
            return View();
        }
    }
}
