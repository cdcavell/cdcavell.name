using System.ComponentModel.DataAnnotations;

namespace cdcavell.Models.Search
{
    /// <summary>
    /// Search Input Model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 10/12/2020 | Initial build |~ 
    /// </revision>
    public class SearchModel
    {
        /// <value>string</value>
        [Required]
        public string SearchRequest { get; set; }
        /// <value>string</value>
        public SearchResponse SearchResponse { get; set; }
        /// <value>ImageResponse</value>
        public SearchResponse ImageResponse { get; set; }
        /// <value>VideoResponse</value>
        public SearchResponse VideoResponse { get; set; }
    }
}
