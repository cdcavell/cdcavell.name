using System;
using System.Net;
using System.Web;

namespace cdcavell.Models.Search
{
    /// <summary>
    /// Search Model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 10/24/2020 | Initial build |~ 
    /// </revision>
    public class SearchModel
    {
        /// <value>HttpStatusCode</value>
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NoContent;
        /// <value>int</value>
        public int Tab { get; set; } = 1;
        /// <value>ResultModel</value>
        public ResultModel SearchResult { get; set; } = new ResultModel();
        /// <value>ResultModel</value>
        public ResultModel ImageResult { get; set; } = new ResultModel();
        /// <value>ResultModel</value>
        public ResultModel VideoResult { get; set; } = new ResultModel();

        private string _searchRequest;
        /// <value>string</value>
        public string SearchRequest 
        {
            get => _searchRequest; 
            set { _searchRequest = HttpUtility.UrlEncode(value.Trim().Clean()); }
                
        }
    }
}
