using AspNetCore.SEOHelper.Sitemap;
using cdcavell.Models.AppSettings;
using CDCavell.ClassLibrary.Commons.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

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
        public void Create()
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
        }
    }
}
