using cdcavell.Models.AppSettings;
using cdcavell.Models.Search;
using CDCavell.ClassLibrary.Web.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Web;

namespace cdcavell.Controllers
{
    /// <summary>
    /// Home controller class
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 10/24/2020 | Initial build |~ 
    /// </revision>
    public class HomeController : ApplicationBaseController<HomeController>
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="logger">ILogger</param>
        /// <param name="webHostEnvironment">IWebHostEnvironment</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        /// <param name="appSettings">AppSettings</param>
        /// <method>
        /// public HomeController(
        ///     ILogger&lt;HomeController&gt; logger,
        ///     IWebHostEnvironment webHostEnvironment,
        ///     IHttpContextAccessor httpContextAccessor,
        ///     AppSettings appSettings
        /// ) : base(logger, webHostEnvironment, httpContextAccessor, appSettings)
        /// </method>
        public HomeController(
            ILogger<HomeController> logger,
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
        /// Revoke external access permissions
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <method>Revoke()</method>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Revoke(string provider)
        {
            switch (provider.Clean().ToLower())
            {
                case "microsoft":
                    break;
                case "google":
                    break;
                case "github":
                    break;
                case "twitter":
                    break;
                case "facebook":
                    break;
                default:
                    return BadRequest();
            }

            ViewBag.Provider = provider.ToLower();
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

        /// <summary>
        /// Search get method
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <method>Search()</method>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Search()
        {
            SearchModel model = new SearchModel();
            return View(model);
        }

        /// <summary>
        /// Handle postback from Search request method
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <method>Search()</method>
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(SearchModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.StatusCode == HttpStatusCode.NoContent)
                {
                    model.StatusCode = HttpStatusCode.NotFound;
                    model.WebResult.StatusCode = HttpStatusCode.NotFound;
                    model.WebResult.StatusMessage = HttpStatusCode.NotFound.ToString();
                    model.ImageResult.StatusCode = HttpStatusCode.NotFound;
                    model.ImageResult.StatusMessage = HttpStatusCode.NotFound.ToString();
                    model.VideoResult.StatusCode = HttpStatusCode.NotFound;
                    model.VideoResult.StatusMessage = HttpStatusCode.NotFound.ToString();

                    Classes.BingCustomSearch search = new Classes.BingCustomSearch(
                        _appSettings.Authentication.BingCustomSearch.Url,
                        _appSettings.Authentication.BingCustomSearch.CustomConfigId,
                        _appSettings.Authentication.BingCustomSearch.SubscriptionKey
                    );

                    model.WebResult = Classes.BingCustomSearch.GetResults("Web", model.SearchRequest);
                    if (model.WebResult.StatusCode == HttpStatusCode.OK)
                    {
                        model.StatusCode = model.WebResult.StatusCode;
                        model.WebActive = "active";
                    }

                    model.ImageResult = Classes.BingCustomSearch.GetResults("Image", model.SearchRequest);
                    if (model.ImageResult.StatusCode == HttpStatusCode.OK)
                    {
                        model.StatusCode = model.ImageResult.StatusCode;
                        if (string.IsNullOrEmpty(model.WebActive))
                            model.ImageActive = "active";
                    }

                    model.VideoResult = Classes.BingCustomSearch.GetResults("Video", model.SearchRequest);
                    if (model.VideoResult.StatusCode == HttpStatusCode.OK)
                    {
                        model.StatusCode = model.VideoResult.StatusCode;
                        if (string.IsNullOrEmpty(model.WebActive) && string.IsNullOrEmpty(model.ImageActive))
                            model.VideoActive = "active";
                    }
                }

                return View(model);
            }

            model = new SearchModel();
            model.StatusCode = HttpStatusCode.BadRequest;
            return View(model);
        }

        /// <summary>
        /// Authorized get method
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <method>Search()</method>
        [Authorize(Policy = "User")]
        [HttpGet]
        public IActionResult Authorized()
        {
            return View();
        }
    }
}
