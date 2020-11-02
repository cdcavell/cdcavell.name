using CDCavell.ClassLibrary.Web.Utilities.Models.BingWebSearchModels;
using System;
using System.Net;
using System.Web;

namespace cdcavell.Models.Home.Search
{
    /// <summary>
    /// Search Model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 10/25/2020 | Initial build |~ 
    /// | Christopher D. Cavell | 1.0.0.1 | 10/28/2020 | Update namespace |~ 
    /// | Christopher D. Cavell | 1.0.0.8 | 11/01/2020 | Bing Search APIs will transition from Azure Cognitive Services to Azure Marketplace on 31 October 2023 [#152](https://github.com/cdcavell/cdcavell.name/issues/152) |~ 
    /// </revision>
    public class SearchModel
    {
        /// <value>HttpStatusCode</value>
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NoContent;
        /// <value>string</value>
        public string WebActive { get; set; } = string.Empty;
        /// <value>string</value>
        public string WebDisabled { get { return (this.WebResult.Items.Count > 0) ? string.Empty : "disabled"; } }
        /// <value>ResultModel</value>
        public ResultModel WebResult { get; set; } = new ResultModel("Web");
        /// <value>string</value>
        public string ImageActive { get; set; } = string.Empty;
        /// <value>string</value>
        public string ImageDisabled { get { return (this.ImageResult.Items.Count > 0) ? string.Empty : "disabled"; } }
        /// <value>ResultModel</value>
        public ResultModel ImageResult { get; set; } = new ResultModel("Image");
        /// <value>string</value>
        public string VideoActive { get; set; } = string.Empty;
        /// <value>string</value>
        public string VideoDisabled { get { return (this.VideoResult.Items.Count > 0) ? string.Empty : "disabled"; } }
        /// <value>ResultModel</value>
        public ResultModel VideoResult { get; set; } = new ResultModel("Video");

        private string _searchRequest;
        /// <value>string</value>
        public string SearchRequest 
        {
            get => _searchRequest; 
            set { _searchRequest = HttpUtility.UrlEncode((value ?? string.Empty).Trim().Clean()); }
                
        }
    }
}
