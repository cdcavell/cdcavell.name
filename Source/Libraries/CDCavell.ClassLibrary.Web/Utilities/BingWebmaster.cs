using CDCavell.ClassLibrary.Web.Http;
using CDCavell.ClassLibrary.Web.Utilities.Models.BingWebmasterModels;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;

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
        /// <method>BingWebmaster(string apiKey)</method>
        public UrlSubmissionQuota GetUrlSubmission(string siteUrl)
        {
            string url = "GetUrlSubmissionQuota?siteUrl="
                + siteUrl.Clean()
                + "&apikey="
                + _apiKey;

            JsonClient client = new JsonClient(_bingWebmasterUrl);
            HttpStatusCode statusCode = client.SendRequest(HttpMethod.Get, url, string.Empty);

            UrlSubmissionQuota quota = new UrlSubmissionQuota();
            if (client.IsResponseSuccess)
            {
                string result = client.GetResponseString();
                result = result.Substring(0, (result.Length - 1));
                result = result.Substring(5);
                quota = JsonConvert.DeserializeObject<UrlSubmissionQuota>(result);
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
        /// <method>SubmitUrl(string siteUrl, string submitUrl)</method>
        public void SubmitUrl(string siteUrl, string submitUrl)
        {
            string postUrl = "SubmitUrl?apikey="
                + _apiKey;

            JsonClient client = new JsonClient(_bingWebmasterUrl);
            HttpStatusCode statusCode = client.SendRequest(
                HttpMethod.Post, 
                postUrl, 
                new { siteUrl = siteUrl.Clean(), url = submitUrl.Clean() }
            );

            if (client.IsResponseSuccess)
            {
                string result = client.GetResponseString();
                result = result.Substring(0, (result.Length - 1));
                result = result.Substring(5);
            }
            
        }
    }
}
