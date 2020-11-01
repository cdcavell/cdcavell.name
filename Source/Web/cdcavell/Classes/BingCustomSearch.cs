using cdcavell.Models.AppSettings;
using cdcavell.Models.Home.Search;
using cdcavell.Models.Home.Search.BingResult;
using CDCavell.ClassLibrary.Commons.Logging;
using CDCavell.ClassLibrary.Web.Http;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Security;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Web;

namespace cdcavell.Classes
{
    /// <summary>
    /// Microsoft Bing Custom Search class
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 10/27/2020 | Initial build |~ 
    /// | Christopher D. Cavell | 1.0.0.1 | 10/28/2020 | Update namespace |~ 
    /// </revision>
    public class BingCustomSearch
    {
        private static string _bingCustomSearchUrl;
        private static string _customConfigId;
        private static string _subscriptionKey;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="url">string</param>
        /// <param name="customConfigId">string</param>
        /// <param name="subscriptionKey">string</param>
        /// <method>BingCustomSearch(string url, string customConfigId, string subscriptionKey)</method>
        public BingCustomSearch(string url, string customConfigId, string subscriptionKey)
        {
            _bingCustomSearchUrl = url;
            _customConfigId = customConfigId;
            _subscriptionKey = subscriptionKey;
        }

        /// <summary>
        /// Return Bing Custom Search request method
        /// </summary>
        /// <param name="searchType">string</param>
        /// <param name="query">string</param>
        /// <returns>ResultModel</returns>
        /// <exception cref="Exception">searchType - excepted (Web, Image or Video)</exception>
        /// <method>Search()</method>
        public static ResultModel GetResults(
            string searchType, 
            string query
        )
        {
            ResultModel results = new ResultModel(searchType);

            // set search query url
            string url = string.Empty;
            switch(results.Type)
            {
                case "Web":
                    url += "search?q=";
                    break;
                case "Image":
                    url += "images/search?q=";
                    break;
                case "Video":
                    url += "videos/search?q=";
                    break;
            }

            url += HttpUtility.UrlEncode(query.Trim().Clean());
            url += "&customconfig=" + _customConfigId;
            url += "&count=" + results.MaxResults;
            url += "&offset=" + ((results.PageNumber * results.MaxResults) - results.MaxResults);

            JsonClient client = new JsonClient(_bingCustomSearchUrl);
            client.AddRequestHeader("Ocp-Apim-Subscription-Key", _subscriptionKey);
            results.StatusCode = client.SendRequest(HttpMethod.Get, url, string.Empty);
            results.StatusMessage = client.StatusCode.ToString();

            if (client.IsResponseSuccess)
            {
                switch (results.Type)
                {
                    case "Web":
                        SearchResponse searchResponse = client.GetResponseObject<SearchResponse>();
                        foreach (SearchPage page in searchResponse.webPages.value)
                        {
                            ItemModel item = new ItemModel();
                            item.ContentUrl = page.url;
                            item.HostPageUrl = page.url;
                            item.Heading = page.name;
                            item.Description = page.snippet;
                            item.DatePublishedCrawled = page.dateLastCrawled;

                            results.Items.Add(item);
                        }
                        break;
                    case "Image":
                        ImagePages imagePages = client.GetResponseObject<ImagePages>();
                        foreach (ImagePage page in imagePages.value)
                        {
                            ItemModel item = new ItemModel();
                            item.ContentUrl = page.contentUrl;
                            item.HostPageUrl = page.hostPageUrl;
                            item.Heading = page.name;
                            item.Description = string.Empty;
                            item.DatePublishedCrawled = page.datePublished;
                            item.ThumbnailUrl = page.thumbnailUrl;
                            item.EncodingFormat = page.encodingFormat;
                            item.isFamilyFriendly = page.isFamilyFriendly;

                            results.Items.Add(item);
                        }
                        break;
                    case "Video":
                        VideoPages videoPages = client.GetResponseObject<VideoPages>();
                        foreach (VideoPage page in videoPages.value)
                        {
                            ItemModel item = new ItemModel();
                            item.ContentUrl = page.contentUrl;
                            item.HostPageUrl = page.hostPageUrl;
                            item.Heading = page.name;
                            item.Description = page.description;
                            item.DatePublishedCrawled = page.datePublished;
                            item.ThumbnailUrl = page.thumbnailUrl;
                            item.EncodingFormat = page.encodingFormat;
                            item.isFamilyFriendly = page.isFamilyFriendly;

                            results.Items.Add(item);
                        }
                        break;
                    default:
                        throw new InvalidParameterException("Invalid search type: " + results.Type);
                }

                if (results.Items.Count == 0)
                {
                    results.StatusCode = HttpStatusCode.NotFound;
                    results.StatusMessage = HttpStatusCode.NotFound.ToString();
                }
            }
            else 
            {
                results.StatusMessage = client.GetResponseString();
            }

            return results;
        }

    }
}
