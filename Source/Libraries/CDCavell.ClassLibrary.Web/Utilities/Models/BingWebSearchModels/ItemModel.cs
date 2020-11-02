using System;

namespace CDCavell.ClassLibrary.Web.Utilities.Models.BingWebSearchModels
{
    /// <summary>
    /// Item Model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.8 | 11/01/2020 | Bing Search APIs will transition from Azure Cognitive Services to Azure Marketplace on 31 October 2023 [#152](https://github.com/cdcavell/cdcavell.name/issues/152) |~ 
    /// </revision>
    public class ItemModel
    {
        /// <value>string</value>
        public string Name { get; set; }
        /// <value>string</value>
        public string Description { get; set; }
        /// <value>string</value>
        public string Snippet { get; set; }
        /// <value>DateTime</value>
        public DateTime DatePublished { get; set; }
        /// <value>string</value>
        public string DatePublishedFormated { get { return this.DatePublished.ToString("MM/dd/yyyy"); } }
        /// <value>DateTime</value>
        public DateTime DateLastCrawled { get; set; }
        /// <value>string</value>
        public string DateLastCrawledFormated { get { return this.DateLastCrawled.ToString("MM/dd/yyyy"); } }
        /// <value>bool</value>
        public bool isFamilyFriendly { get; set; }
        /// <value>string</value>
        public string Url { get; set; }
        /// <value>string</value>
        public string DisplayUrl { get; set; }
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
