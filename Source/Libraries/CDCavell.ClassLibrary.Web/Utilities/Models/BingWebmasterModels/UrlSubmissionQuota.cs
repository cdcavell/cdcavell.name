using System.Net;

namespace CDCavell.ClassLibrary.Web.Utilities.Models.BingWebmasterModels
{
    /// <summary>
    /// Microsoft Bing Webmaster Url Submission Quota
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.7 | 10/31/2020 | Integrate Bing’s Adaptive URL submission API with your website [#144](https://github.com/cdcavell/cdcavell.name/issues/144) |~ 
    /// </revision>
    public class UrlSubmissionQuota
    {
        /// <value>string</value>
        public string __type { get; set; }
        /// <value>int</value>
        public int DailyQuota { get; set; }
        /// <value>int</value>
        public int MonthlyQuota { get; set; }
        /// <value>HttpStatusCode</value>
        public HttpStatusCode StatusCode { get; set; }
        /// <value>string</value>
        public string StatusMessage { get; set; }
    }
}
