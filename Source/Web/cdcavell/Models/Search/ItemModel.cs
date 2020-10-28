using System;

namespace cdcavell.Models.Search
{
    /// <summary>
    /// Item Model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 10/23/2020 | Initial build |~ 
    /// </revision>
    public class ItemModel
    {
        /// <value>string</value>
        public string Heading { get; set; }
        /// <value>string</value>
        public string Description { get; set; }
        /// <value>DateTime</value>
        public DateTime DatePublishedCrawled { get; set; }
        /// <value>string</value>
        public string DatePublishedCrawledFormated { get { return this.DatePublishedCrawled.ToString("MM/dd/yyyy"); } }
        /// <value>bool</value>
        public bool isFamilyFriendly { get; set; }
        /// <value>string</value>
        public string ContentUrl { get; set; }
        /// <value>string</value>
        public string HostPageUrl { get; set; }
        /// <value>string</value>
        public string ThumbnailUrl { get; set; } = string.Empty;
        /// <value>string</value>
        public string EncodingFormat { get; set; }
    }
}
