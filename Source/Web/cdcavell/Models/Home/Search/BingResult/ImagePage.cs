using System;

namespace cdcavell.Models.Home.Search.BingResult
{
    /// <summary>
    /// Bing Custom Search ImagePage model
    /// &lt;br /&gt;&lt;br /&gt;
    /// https://docs.microsoft.com/en-us/azure/cognitive-services/bing-custom-search/call-endpoint-csharp
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 10/23/2020 | Initial build |~ 
    /// | Christopher D. Cavell | 1.0.0.1 | 10/28/2020 | Update namespace |~ 
    /// </revision>
    public class ImagePage
    {
        /// <value>string</value>
        public string name { get; set; }
        /// <value>string</value>
        public string thumbnailUrl { get; set; }
        /// <value>DateTime</value>
        public DateTime datePublished { get; set; }
        /// <value>bool</value>
        public bool isFamilyFriendly { get; set; }
        /// <value>string</value>
        public string contentUrl { get; set; }
        /// <value>string</value>
        public string hostPageUrl { get; set; }
        /// <value>string</value>
        public string encodingFormat { get; set; }
        /// <value>int</value>
        public int width { get; set; }
        /// <value>int</value>
        public int height { get; set; }
        /// <value>thumbnail</value>
        public thumbnail thumbnail { get; set; }
        /// <value>string</value>
        public string imageId { get; set; }
        /// <value>string</value>
        public string accentColor { get; set; }
    }
}
