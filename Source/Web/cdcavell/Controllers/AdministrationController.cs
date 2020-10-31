using AspNetCore.SEOHelper.Sitemap;
using cdcavell.Models.AppSettings;
using CDCavell.ClassLibrary.Web.Mvc.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

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
        public IActionResult CreateSitemap()
        {
            var url = "https://cdcavell.name";
            var list = new List<SitemapNode>();
            list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.9, Url = url, Frequency = SitemapFrequency.Daily });
            list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = url + "/Home/Index", Frequency = SitemapFrequency.Daily });
            list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = url + "/Home/Search", Frequency = SitemapFrequency.Daily });
            list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.7, Url = url + "/Home/PrivacyPolicy", Frequency = SitemapFrequency.Yearly });
            list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.7, Url = url + "/Home/TermsOfService", Frequency = SitemapFrequency.Yearly });
            list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.6, Url = url + "/Home/Revoke?provider=microsoft", Frequency = SitemapFrequency.Yearly });
            list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.6, Url = url + "/Home/Revoke?provider=google", Frequency = SitemapFrequency.Yearly });
            list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.6, Url = url + "/Home/Revoke?provider=github", Frequency = SitemapFrequency.Yearly });
            list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.6, Url = url + "/Home/Revoke?provider=twitter", Frequency = SitemapFrequency.Yearly });
            list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.6, Url = url + "/Home/Revoke?provider=facebook", Frequency = SitemapFrequency.Yearly });

            new SitemapDocument().CreateSitemapXML(list, _webHostEnvironment.ContentRootPath);
            return Redirect("/sitemap.xml");  // "sitemap.xml file should be created in root directory"
        }
    }
}
