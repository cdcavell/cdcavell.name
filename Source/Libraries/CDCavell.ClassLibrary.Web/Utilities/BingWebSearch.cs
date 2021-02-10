using CDCavell.ClassLibrary.Web.Http;
using CDCavell.ClassLibrary.Web.Utilities.Models.BingWebSearchModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CDCavell.ClassLibrary.Web.Utilities
{
    /// <summary>
    /// Microsoft Bing Custom Search class
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.8 | 11/01/2020 | Bing Search APIs will transition from Azure Cognitive Services to Azure Marketplace on 31 October 2023 [#152](https://github.com/cdcavell/cdcavell.name/issues/152) |~ 
    /// | Christopher D. Cavell | 1.0.3.1 | 02/07/2021 | User Authorization Web Service |~ 
    /// </revision>
    public class BingWebSearch
    {
        private static string _baseUri = "https://api.bing.microsoft.com/v7.0/custom/";
        private static string _subscriptionKey = "<your subscription key will go here>";
        private static string _customConfigId = "<your custom config id will go here>";

        private const string QUERY_PARAMETER = "search?q=";  // Required
        private const string MKT_PARAMETER = "&mkt=";  // Strongly suggested
        private const string RESPONSE_FILTER_PARAMETER = "&responseFilter=";
        private const string COUNT_PARAMETER = "&count=";
        private const string OFFSET_PARAMETER = "&offset=";
        private const string FRESHNESS_PARAMETER = "&freshness=";
        private const string SAFE_SEARCH_PARAMETER = "&safeSearch=";
        private const string TEXT_DECORATIONS_PARAMETER = "&textDecorations=";
        private const string TEXT_FORMAT_PARAMETER = "&textFormat=";
        private const string ANSWER_COUNT = "&answerCount=";
        private const string PROMOTE = "&promote=";
        private const string CUSTOM_CONFIG = "&customconfig=";

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="subscriptionKey">string</param>
        /// <param name="customConfigId">string</param>
        /// <method>BingWebSearch(string subscriptionKey, string customConfigId)</method>
        public BingWebSearch(string subscriptionKey, string customConfigId)
        {
            _subscriptionKey = subscriptionKey.Trim().Clean();
            _customConfigId = customConfigId.Trim().Clean();
        }

        /// <summary>
        /// Return Bing Web Search results
        /// </summary>
        /// <param name="searchType">string</param>
        /// <param name="query">string</param>
        /// <returns>Task&lt;ResultModel&gt;</returns>
        /// <exception cref="Exception">searchType - excepted (Web, Image or Video)</exception>
        /// <method>Search(string searchType, string query)</method>
        public async Task<ResultModel> Search(string searchType, string query)
        {
            ResultModel results = new ResultModel(searchType.Trim().Clean());

            // set search query url
            string queryString = string.Empty;
            switch (results.Type)
            {
                case "Web":
                    queryString += QUERY_PARAMETER;
                    break;
                case "Image":
                    queryString += "images/" + QUERY_PARAMETER;
                    break;
                case "Video":
                    queryString += "videos/" + QUERY_PARAMETER;
                    break;
            }

            queryString += Uri.EscapeDataString(query.Trim().Clean());
            queryString += CUSTOM_CONFIG + _customConfigId;
            queryString += MKT_PARAMETER + "en-US";
            queryString += TEXT_DECORATIONS_PARAMETER + Boolean.TrueString;
            queryString += COUNT_PARAMETER + results.MaxResults;
            queryString += OFFSET_PARAMETER + ((results.PageNumber * results.MaxResults) - results.MaxResults);

            JsonClient client = new JsonClient(_baseUri);
            client.AddRequestHeader("Ocp-Apim-Subscription-Key", _subscriptionKey);
            results.StatusCode = await client.SendRequest(HttpMethod.Get, queryString, string.Empty);

            // wait 1 second to prevent throttling
            Thread.Sleep(1000);
            
            results.StatusMessage = client.GetResponseString();

            if (client.IsResponseSuccess)
            {
                Dictionary<string, object> resultDictionary = client.GetResponseObject<Dictionary<string, object>>();
                switch (results.Type)
                {
                    case "Web":
                        JObject webResultObject = (JObject)resultDictionary.Where(x => x.Key == "webPages").Select(x => x.Value).FirstOrDefault();
                        results.Items = ((JArray)webResultObject["value"]).ToObject<List<ItemModel>>();
                        results.StatusMessage = client.StatusCode.ToString();
                        break;
                    case "Image":
                        Object[] imageArray = resultDictionary.Where(x => x.Key == "value").Select(x => x.Value).ToArray();
                        results.Items = ((JArray)imageArray[0]).ToObject<List<ItemModel>>();
                        results.StatusMessage = client.StatusCode.ToString();
                        break;
                    case "Video":
                        Object[] videoArray = resultDictionary.Where(x => x.Key == "value").Select(x => x.Value).ToArray();
                        results.Items = ((JArray)videoArray[0]).ToObject<List<ItemModel>>();
                        results.StatusMessage = client.StatusCode.ToString();
                        break;
                }
            }

            return results;
        }

    }
}
