using AspNetCore.SEOHelper.Sitemap;
using cdcavell.Models.AppSettings;
using CDCavell.ClassLibrary.Web.Mvc.Controllers;
using CDCavell.ClassLibrary.Web.Utilities;
using CDCavell.ClassLibrary.Web.Utilities.Models.BingWebmasterModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace cdcavell.Controllers
{
    /// <summary>
    /// Administration Controller
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 10/19/2020 | Initial build |~ 
    /// | Christopher D. Cavell | 1.0.0.6 | 10/31/2020 | Convert Sitemap class to build sitemap.xml dynamic based on existing controllers in project [#145](https://github.com/cdcavell/cdcavell.name/issues/145) |~ 
    /// | Christopher D. Cavell | 1.0.0.7 | 10/31/2020 | Integrate Bing’s Adaptive URL submission API with your website [#144](https://github.com/cdcavell/cdcavell.name/issues/144) |~ 
    /// </revision>
    [Authorize(Policy = "Administration")]
    public class AdministrationController : ApplicationBaseController<AdministrationController>
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="logger">ILogger&lt;AdministrationController&gt;</param>
        /// <param name="webHostEnvironment">IWebHostEnvironment</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        /// <param name="appSettings">AppSettings</param>
        /// <method>
        /// AccountController(
        ///     ILogger&lt;AdministrationController&gt; logger, 
        ///     IWebHostEnvironment webHostEnvironment, 
        ///     IHttpContextAccessor httpContextAccessor,
        ///     AppSettings appSettings
        /// ) : base(logger, webHostEnvironment, httpContextAccessor, appSettings)
        /// </method>
        public AdministrationController(
            ILogger<AdministrationController> logger,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            AppSettings appSettings
        ) : base(logger, webHostEnvironment, httpContextAccessor, appSettings)
        {
        }

        /// <summary>
        /// Index method
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <method>Index()</method>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// sitemap.xml in ASP.NET Core
        /// &lt;br /&gt;&lt;br /&gt;
        /// https://www.c-sharpcorner.com/article/create-and-configure-sitemap-xml-in-asp-net-core/
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <method>public IActionResult CreateSitemap()</method>
        [HttpGet]
        public IActionResult SubmitUrl()
        {
            string siteUrl = "https://cdcavell.name";

            Assembly asm = Assembly.GetExecutingAssembly();

            var controllerActionList = asm.GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Select(x => new { Controller = x.DeclaringType.Name, Action = x.Name, ReturnType = x.ReturnType.Name, Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", string.Empty))) })
                .ToList()
                .Where(x => x.Attributes.Contains("AllowAnonymous") && x.Attributes.Contains("HttpGet"))
                .Where(x => !x.Controller.Equals("AccountController"))
                .Where(x => !x.Controller.Equals("HomeController") || !x.Action.Equals("WithdrawConsent"))
                .OrderBy(x => x.Controller)
                .OrderBy(x => x.Action)
                .ToList();

            BingWebmaster bingWebmaster = new BingWebmaster(_appSettings.Authentication.BingWebmaster.ApiKey);
            UrlSubmissionQuota quota = bingWebmaster.GetUrlSubmission(siteUrl);

            bingWebmaster.SubmitUrl(siteUrl, siteUrl + "/Home/Index");

            return View();
        }
    }
}
