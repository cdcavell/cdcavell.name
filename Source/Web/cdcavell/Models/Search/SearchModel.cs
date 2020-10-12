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
    /// | Christopher D. Cavell | 1.0.0 | 10/10/2020 | Initial build |~ 
    /// </revision>
    public class SearchModel
    {
        /// <value>string</value>
        [Required]
        public string SearchRequest { get; set; }
        /// <value>string</value>
        public string MessageClass { get; set; } = "text-info";
        /// <value>string</value>
        public string Message { get; set; }
        /// <value>SearchResponse</value>
        public SearchResponse SearchResponse { get; set; }
    }
}
