using cdcavell.Models.AppSettings;
using cdcavell.Models.Search;
using CDCavell.ClassLibrary.Web.Http;
using CDCavell.ClassLibrary.Web.Mvc.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
    /// | Christopher D. Cavell | 1.0.0 | 10/17/2020 | Initial build |~ 
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
                string request = HttpUtility.UrlEncode(model.SearchRequest).Clean();
                JsonClient client = new JsonClient(_appSettings.Authentication.BingCustomSearch.Url);
                HttpStatusCode statusCode = client.StatusCode;

                // --------------------------- get search results ---------------------------
                int resultCount = 10;
                int offset = (model.SearchResponse == null) ? 0 : ((model.SearchResponse.PageNumber * resultCount) - resultCount);

                // get search results
                string searchUrl = "search?q=" + request
                    + "&customconfig=" + _appSettings.Authentication.BingCustomSearch.CustomConfigId
                    + "&count=" + resultCount + "&offset=" + offset;

                client.AddRequestHeader("Ocp-Apim-Subscription-Key", _appSettings.Authentication.BingCustomSearch.SubscriptionKey);
                statusCode = client.SendRequest(HttpMethod.Get, searchUrl, string.Empty);

                if (client.IsResponseSuccess)
                {
                    SearchResponse searchResponse = client.GetResponseObject<SearchResponse>();
                    searchResponse.PageNumber = model.SearchResponse.PageNumber;

                    model.SearchResponse = searchResponse;
                    model.SearchResponse.StatusCode = client.StatusCode;
                    model.SearchResponse.StatusMessage = client.StatusCode.ToString();
                    model.SearchResponse.webPages.currentOffset = offset;
                    model.SearchResponse.webPages.nextOffset = offset + resultCount;

                    model.SearchResponse.webPages.totalEstimatedMatches = (searchResponse.webPages.totalEstimatedMatches / resultCount) < 40 ? searchResponse.webPages.totalEstimatedMatches : (40 * resultCount);
                    model.SearchResponse.TotalPages = (searchResponse.webPages.totalEstimatedMatches < resultCount) ? 1 : (searchResponse.webPages.totalEstimatedMatches / resultCount);
                }
                else 
                {
                    model.SearchResponse.StatusCode = client.StatusCode;
                    model.SearchResponse.StatusMessage = client.GetResponseString();
                }

                // --------------------------- get image results ---------------------------
                resultCount = 20;
                offset = (model.ImageResponse == null) ? 0 : ((model.ImageResponse.PageNumber * resultCount) - resultCount);

                // get search results
                searchUrl = "images/search?q=" + request
                    + "&customconfig=" + _appSettings.Authentication.BingCustomSearch.CustomConfigId
                    + "&count=" + resultCount + "&offset=" + offset;

                client.AddRequestHeader("Ocp-Apim-Subscription-Key", _appSettings.Authentication.BingCustomSearch.SubscriptionKey);
                statusCode = client.SendRequest(HttpMethod.Get, searchUrl, string.Empty);

                if (client.IsResponseSuccess)
                {
                    ImageResponse imageResponse = client.GetResponseObject<ImageResponse>();
                    imageResponse.PageNumber = model.ImageResponse.PageNumber;

                    model.ImageResponse = imageResponse;
                    model.ImageResponse.StatusCode = client.StatusCode;
                    model.ImageResponse.StatusMessage = client.StatusCode.ToString();

                    model.ImageResponse.totalEstimatedMatches = (imageResponse.totalEstimatedMatches / resultCount) < 40 ? imageResponse.totalEstimatedMatches : (40 * resultCount);
                    model.ImageResponse.TotalPages = (imageResponse.totalEstimatedMatches < resultCount) ? 1 : (imageResponse.totalEstimatedMatches / resultCount);
                }
                else
                {
                    model.ImageResponse.StatusCode = client.StatusCode;
                    model.ImageResponse.StatusMessage = client.GetResponseString();
                }

                // --------------------------- get video results ---------------------------
                resultCount = 10;
                offset = (model.VideoResponse == null) ? 0 : ((model.VideoResponse.PageNumber * resultCount) - resultCount);

                // get video results
                searchUrl = "videos/search?q=" + request
                    + "&customconfig=" + _appSettings.Authentication.BingCustomSearch.CustomConfigId
                    + "&count=" + resultCount + "&offset=" + offset;

                client.AddRequestHeader("Ocp-Apim-Subscription-Key", _appSettings.Authentication.BingCustomSearch.SubscriptionKey);
                statusCode = client.SendRequest(HttpMethod.Get, searchUrl, string.Empty);

                if (client.IsResponseSuccess)
                {
                    VideoResponse videoResponse = client.GetResponseObject<VideoResponse>();
                    videoResponse.PageNumber = model.VideoResponse.PageNumber;

                    model.VideoResponse = videoResponse;
                    model.VideoResponse.StatusCode = client.StatusCode;
                    model.VideoResponse.StatusMessage = client.StatusCode.ToString();
                    model.VideoResponse.currentOffset = offset;
                    model.VideoResponse.nextOffset = offset + resultCount;

                    model.VideoResponse.totalEstimatedMatches = (videoResponse.totalEstimatedMatches / resultCount) < 40 ? videoResponse.totalEstimatedMatches : (40 * resultCount);
                    model.VideoResponse.TotalPages = (videoResponse.totalEstimatedMatches < resultCount) ? 1 : (videoResponse.totalEstimatedMatches / resultCount);
                }
                else
                {
                    model.VideoResponse.StatusCode = client.StatusCode;
                    model.VideoResponse.StatusMessage = client.GetResponseString();
                }

            }
            else
            {
                model.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(ModelState);
            }

            model.StatusCode = HttpStatusCode.OK;
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
