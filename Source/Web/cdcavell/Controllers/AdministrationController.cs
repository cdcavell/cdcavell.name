using AspNetCore.SEOHelper.Sitemap;
using cdcavell.Data;
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
        /// <param name="dbContext">CDCavellDbContext</param>
        /// <method>
        /// AccountController(
        ///     ILogger&lt;AdministrationController&gt; logger, 
        ///     IWebHostEnvironment webHostEnvironment, 
        ///     IHttpContextAccessor httpContextAccessor,
        ///     AppSettings appSettings,
        ///     CDCavellDbContext dbContext
        /// ) : base(logger, webHostEnvironment, httpContextAccessor, appSettings, dbContext)
        /// </method>
        public AdministrationController(
            ILogger<AdministrationController> logger,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            AppSettings appSettings,
            CDCavellDbContext dbContext
        ) : base(logger, webHostEnvironment, httpContextAccessor, appSettings, dbContext)
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
    }
}
