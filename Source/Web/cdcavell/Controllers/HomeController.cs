using cdcavell.Models.AppSettings;
using cdcavell.Models.Home;
using cdcavell.Models.Home.Search;
using CDCavell.ClassLibrary.Web.Services.Data;
using CDCavell.ClassLibrary.Web.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace cdcavell.Controllers
{
    /// <summary>
    /// Home controller class
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 10/28/2020 | Initial build |~ 
    /// | Christopher D. Cavell | 1.0.0.1 | 10/28/2020 | Update namespace |~ 
    /// | Christopher D. Cavell | 1.0.0.1 | 10/29/2020 | Remove YouTubeVideo from Index |~ 
    /// | Christopher D. Cavell | 1.0.0.5 | 10/30/2020 | EU General Data Protection Regulation (GDPR) support in ASP.NET Core [#161](https://github.com/cdcavell/cdcavell.name/issues/161) |~
    /// | Christopher D. Cavell | 1.0.0.7 | 10/31/2020 | Integrate Bing’s Adaptive URL submission API with your website [#144](https://github.com/cdcavell/cdcavell.name/issues/144) |~ 
    /// | Christopher D. Cavell | 1.0.0.8 | 11/01/2020 | Bing Search APIs will transition from Azure Cognitive Services to Azure Marketplace on 31 October 2023 [#152](https://github.com/cdcavell/cdcavell.name/issues/152) |~ 
    /// | Christopher D. Cavell | 1.0.0.9 | 11/04/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// | Christopher D. Cavell | 1.0.3.1 | 02/07/2021 | User Authorization Web Service |~ 
    /// | Christopher D. Cavell | 1.0.3.3 | 03/07/2021 | User Authorization Web Service |~ 
    /// </revision>
    public class HomeController : ApplicationBaseController<HomeController>
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="logger">ILogger</param>
        /// <param name="webHostEnvironment">IWebHostEnvironment</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        /// <param name="authorizationService">IAuthorizationService</param>
        /// <param name="appSettings">AppSettings</param>
        /// <param name="dbContext">AuthorizationDbContext</param>
        /// <method>
        /// public HomeController(
        ///     ILogger&lt;HomeController&gt; logger,
        ///     IWebHostEnvironment webHostEnvironment,
        ///     IHttpContextAccessor httpContextAccessor,
        ///     IAuthorizationService authorizationService,
        ///     AppSettings appSettings,
        ///     AuthorizationDbContext dbContext
        /// ) : base(logger, webHostEnvironment, httpContextAccessor, authorizationService, appSettings, dbContext)
        /// </method>
        public HomeController(
            ILogger<HomeController> logger,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            AppSettings appSettings,
            AuthorizationDbContext dbContext
        ) : base(logger, webHostEnvironment, httpContextAccessor, authorizationService, appSettings, dbContext)
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
            IndexModel model = new IndexModel();
            return View(model);
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
        /// Withdraw cookie consent
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <method>WithdrawConsent()</method>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult WithdrawConsent()
        {
            HttpContext.Features.Get<ITrackingConsentFeature>().WithdrawConsent();
            return RedirectToAction("Index", "Home");
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
        /// <returns>Task&lt;IActionResult&gt;</returns>
        /// <method>Search()</method>
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(SearchModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.StatusCode == HttpStatusCode.NoContent)
                {
                    model.StatusCode = HttpStatusCode.NotFound;
                    model.WebActive = string.Empty;
                    model.WebResult.StatusCode = HttpStatusCode.NotFound;
                    model.WebResult.StatusMessage = HttpStatusCode.NotFound.ToString();
                    model.ImageActive = string.Empty;
                    model.ImageResult.StatusCode = HttpStatusCode.NotFound;
                    model.ImageResult.StatusMessage = HttpStatusCode.NotFound.ToString();
                    model.VideoActive = string.Empty;
                    model.VideoResult.StatusCode = HttpStatusCode.NotFound;
                    model.VideoResult.StatusMessage = HttpStatusCode.NotFound.ToString();

                    if (!string.IsNullOrEmpty(model.SearchRequest.Trim()))
                    {
                        BingWebSearch bingWebSearch = new BingWebSearch(
                            _appSettings.Application.BingWebSearch.SubscriptionKey,
                            _appSettings.Application.BingWebSearch.CustomConfigId
                        );

                        model.WebResult = await bingWebSearch.Search("Web", model.SearchRequest);
                        if (model.WebResult.StatusCode == HttpStatusCode.OK)
                        {
                            model.StatusCode = model.WebResult.StatusCode;
                            model.WebActive = "active";
                        }


                        model.ImageResult = await bingWebSearch.Search("Image", model.SearchRequest);
                        if (model.ImageResult.StatusCode == HttpStatusCode.OK)
                        {
                            model.StatusCode = model.ImageResult.StatusCode;
                            if (string.IsNullOrEmpty(model.WebActive))
                                model.ImageActive = "active";
                        }

                        model.VideoResult = await bingWebSearch.Search("Video", model.SearchRequest);
                        if (model.VideoResult.StatusCode == HttpStatusCode.OK)
                        {
                            model.StatusCode = model.VideoResult.StatusCode;
                            if (string.IsNullOrEmpty(model.WebActive) && string.IsNullOrEmpty(model.ImageActive))
                                model.VideoActive = "active";
                        }
                    }
                }

                return View(model);
            }

            model = new SearchModel();
            model.StatusCode = HttpStatusCode.BadRequest;
            return View(model);
        }
    }
}
