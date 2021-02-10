using CDCavell.ClassLibrary.Web.Http;
using CDCavell.ClassLibrary.Web.Utilities.Models.BingWebmasterModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CDCavell.ClassLibrary.Web.Utilities
{
    /// <summary>
    /// Microsoft Bing Webmaster class
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.7 | 10/31/2020 | Integrate Bing’s Adaptive URL submission API with your website [#144](https://github.com/cdcavell/cdcavell.name/issues/144) |~ 
    /// | Christopher D. Cavell | 1.0.3.1 | 02/07/2021 | User Authorization Web Service |~ 
    /// </revision>
    public class BingWebmaster
    {
        private static string _bingWebmasterUrl;
        private static string _apiKey;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="apiKey">string</param>
        /// <method>BingWebmaster(string apiKey)</method>
        public BingWebmaster(string apiKey)
        {
            _bingWebmasterUrl = "https://ssl.bing.com/webmaster/api.svc/json/";
            _apiKey = apiKey;
        }

        /// <summary>
        /// Get url submission quota
        /// </summary>
        /// <returns>Task&lt;UrlSubmissionQuota&gt;</returns>
        /// <method>BingWebmaster(string apiKey)</method>
        public async Task<UrlSubmissionQuota> GetUrlSubmission(string siteUrl)
        {
            string url = "GetUrlSubmissionQuota?siteUrl="
                + siteUrl.Clean()
                + "&apikey="
                + _apiKey;

            JsonClient client = new JsonClient(_bingWebmasterUrl);
            HttpStatusCode statusCode = await client.SendRequest(HttpMethod.Get, url, string.Empty);

            UrlSubmissionQuota quota = new UrlSubmissionQuota();
            if (client.IsResponseSuccess)
            {
                Dictionary<string, object> results = client.GetResponseObject<Dictionary<string, object>>();
                if (results.Count > 0)
                    quota = JsonConvert.DeserializeObject<UrlSubmissionQuota>(results.ElementAt(0).Value.ToString());

                quota.StatusCode = statusCode;
                quota.StatusMessage = statusCode.ToString();
            }
            else
            {
                quota.StatusCode = statusCode;
                quota.StatusMessage = client.GetResponseString();
            }

            return quota;
        }

        /// <summary>
        ///Submit url to Bing
        /// </summary>
        /// <param name="siteUrl">string</param>
        /// <param name="submitUrl">string</param>
        /// <returns>Task&lt;HttpStatusCode&gt;</returns>
        /// <method>SubmitUrl(string siteUrl, string submitUrl)</method>
        public async Task<HttpStatusCode> SubmitUrl(string siteUrl, string submitUrl)
        {
            string postUrl = "SubmitUrl?apikey="
                + _apiKey;

            JsonClient client = new JsonClient(_bingWebmasterUrl);
            HttpStatusCode statusCode = await client.SendRequest(
                HttpMethod.Post, 
                postUrl, 
                new { siteUrl = siteUrl.Clean(), url = submitUrl.Clean() }
            );

            return statusCode;
        }
    }
}
