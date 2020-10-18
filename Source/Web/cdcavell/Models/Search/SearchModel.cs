using System.ComponentModel.DataAnnotations;
using System.Net;

namespace cdcavell.Models.Search
{
    /// <summary>
    /// Search Input Model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 10/17/2020 | Initial build |~ 
    /// </revision>
    public class SearchModel
    {
        /// <value>string</value>
        [Required]
        public string SearchRequest { get; set; }
        /// <value>int</value>
        public int Tab { get; set; } = 1;
        /// <value>string</value>
        public SearchResponse SearchResponse { get; set; } = new SearchResponse();
        /// <value>ImageResponse</value>
        public ImageResponse ImageResponse { get; set; } = new ImageResponse();
        /// <value>VideoResponse</value>
        public VideoResponse VideoResponse { get; set; } = new VideoResponse();
        /// <value>HttpStatusCode</value>
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NoContent;
    }
}
