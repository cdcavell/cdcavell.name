using System.ComponentModel.DataAnnotations;
using System.Net;

namespace cdcavell.Models.Search
{
    /// <summary>
    /// Search Model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 10/23/2020 | Initial build |~ 
    /// </revision>
    public class SearchModel
    {
        /// <value>HttpStatusCode</value>
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NoContent;
        /// <value>string</value>
        public string SearchRequest { get; set; }
        /// <value>int</value>
        public int Tab { get; set; } = 1;
        /// <value>ResultModel</value>
        public ResultModel SearchResult { get; set; } = new ResultModel();
        /// <value>ResultModel</value>
        public ResultModel ImageResult { get; set; } = new ResultModel();
        /// <value>ResultModel</value>
        public ResultModel VideoResult { get; set; } = new ResultModel();
    }
}
