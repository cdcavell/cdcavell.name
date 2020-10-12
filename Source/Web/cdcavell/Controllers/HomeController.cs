using cdcavell.Models.AppSettings;
using cdcavell.Models.Search;
using CDCavell.ClassLibrary.Web.Http;
using CDCavell.ClassLibrary.Web.Mvc.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
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
    /// | Christopher D. Cavell | 1.0.0 | 10/12/2020 | Initial build |~ 
    /// </revision>
    public class HomeController : WebBaseController<HomeController>
    {
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="logger">ILogger</param>
        /// <param name="webHostEnvironment">IWebHostEnvironment</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        /// <param name="appSettings">AppSettings</param>
        /// <method>HomeController(ILogger&lt;HomeController&gt; logger, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)</method>
        public HomeController(
            ILogger<HomeController> logger,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            AppSettings appSettings
        ) : base(logger, webHostEnvironment, httpContextAccessor)
        {
            _appSettings = appSettings;
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
                string request = HttpUtility.UrlEncode(model.SearchRequest).Clean();
                JsonClient client = new JsonClient(_appSettings.Authentication.BingCustomSearch.Url);
                HttpStatusCode statusCode = client.StatusCode;

                // get search results
                string searchUrl = "search?q=" + request
                    + "&customconfig=" + _appSettings.Authentication.BingCustomSearch.CustomConfigId;

                client.AddRequestHeader("Ocp-Apim-Subscription-Key", _appSettings.Authentication.BingCustomSearch.SubscriptionKey);
                statusCode = client.SendRequest(HttpMethod.Get, searchUrl, string.Empty);

                if (client.IsResponseSuccess)
                {
                    SearchResponse searchResponse = client.GetResponseObject<SearchResponse>();

                    model.SearchResponse = searchResponse;
                    model.SearchResponse.StatusCode = client.StatusCode;
                    model.SearchResponse.StatusMessage = client.StatusCode.ToString();
                }
                else 
                {
                    model.SearchResponse.StatusCode = client.StatusCode;
                    model.SearchResponse.StatusMessage = client.GetResponseString();
                }
            }
            else
            {
                model.SearchResponse = null;
                model.ImageResponse = null;
                model.VideoResponse = null;
            }

            return View(model);
        }
    }
}
