using AspNetCore.SEOHelper.Sitemap;
using cdcavell.Data;
using cdcavell.Models.AppSettings;
using CDCavell.ClassLibrary.Commons.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace cdcavell.Classes
{
    /// <summary>
    /// Sitemap builder class
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 10/28/2020 | Initial build |~ 
    /// | Christopher D. Cavell | 1.0.0.6 | 10/31/2020 | Convert Sitemap class to build sitemap.xml dynamic based on existing controllers in project [#145](https://github.com/cdcavell/cdcavell.name/issues/145) |~ 
    /// | Christopher D. Cavell | 1.0.0.7 | 10/31/2020 | Integrate Bing’s Adaptive URL submission API with your website [#144](https://github.com/cdcavell/cdcavell.name/issues/144) |~ 
    /// </revision>
    public class Sitemap
    {
        private Logger _logger;
        private IWebHostEnvironment _webHostEnvironment;
        private AppSettings _appSettings;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="logger">ILogger&lt;AdministrationController&gt;</param>
        /// <param name="webHostEnvironment">IWebHostEnvironment</param>
        /// <param name="appSettings">AppSettings</param>
        /// <method>
        /// Sitemap(
        ///     Logger logger, 
        ///     IWebHostEnvironment webHostEnvironment, 
        ///     AppSettings appSettings
        /// )
        /// </method>
        public Sitemap(
            Logger logger,
            IWebHostEnvironment webHostEnvironment,
            AppSettings appSettings
        ) 
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _appSettings = appSettings;
        }

        /// <summary>
        /// Create sitemap.xml in ASP.NET Core
        /// &lt;br /&gt;&lt;br /&gt;
        /// https://www.c-sharpcorner.com/article/create-and-configure-sitemap-xml-in-asp-net-core/
        /// </summary>
        /// <method>public void Create()</method>
        public void Create(CDCavellDbContext dbContext)
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            var controllerActionList = asm.GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Select(x => new { Controller = x.DeclaringType.Name, Action = x.Name, ReturnType = x.ReturnType.Name, Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", string.Empty))) })
                .ToList()
                .Where(x => x.Attributes.Contains("AllowAnonymous") && x.Attributes.Contains("HttpGet"))
                .Where(x => !x.Controller.Equals("AccountController"))
                .Where(x => !x.Controller.Equals("HomeController") || !x.Action.Equals("WithdrawConsent"))
                .ToList();


            var url = "https://cdcavell.name";
            var list = new List<SitemapNode>();
            list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.9, Url = url, Frequency = SitemapFrequency.Daily });

            foreach (var controllerAction in controllerActionList)
            {
                if (controllerAction.Controller.Equals("HomeController") && controllerAction.Action.Equals("Revoke"))
                {
                    list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.6, Url = url + "/" + controllerAction.Controller.Replace("Controller", string.Empty) + "/" + controllerAction.Action + "?provider=microsoft", Frequency = SitemapFrequency.Yearly });
                    list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.6, Url = url + "/" + controllerAction.Controller.Replace("Controller", string.Empty) + "/" + controllerAction.Action + "?provider=google", Frequency = SitemapFrequency.Yearly });
                    list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.6, Url = url + "/" + controllerAction.Controller.Replace("Controller", string.Empty) + "/" + controllerAction.Action + "?provider=github", Frequency = SitemapFrequency.Yearly });
                    list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.6, Url = url + "/" + controllerAction.Controller.Replace("Controller", string.Empty) + "/" + controllerAction.Action + "?provider=twitter", Frequency = SitemapFrequency.Yearly });
                    list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.6, Url = url + "/" + controllerAction.Controller.Replace("Controller", string.Empty) + "/" + controllerAction.Action + "?provider=facebook", Frequency = SitemapFrequency.Yearly });
                }
                else
                {
                    list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.7, Url = url + "/" + controllerAction.Controller.Replace("Controller", string.Empty) + "/" + controllerAction.Action, Frequency = SitemapFrequency.Daily });
                    int count = SiteMap.GetCount(controllerAction.Controller.Replace("Controller", string.Empty), controllerAction.Action, dbContext);
                    if (count == 0)
                    {
                        SiteMap siteMap = new SiteMap();
                        siteMap.Controller = controllerAction.Controller.Replace("Controller", string.Empty);
                        siteMap.Action = controllerAction.Action;

                        dbContext.Add(siteMap);
                        dbContext.SaveChanges();
                    }
                }
            }

            new SitemapDocument().CreateSitemapXML(list, _webHostEnvironment.ContentRootPath);
        }
    }
}
