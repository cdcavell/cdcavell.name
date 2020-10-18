using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cdcavell.Models.Search
{
    /// <summary>
    /// Bing Custom Search VideoPage model
    /// &lt;br /&gt;&lt;br /&gt;
    /// https://docs.microsoft.com/en-us/azure/cognitive-services/bing-custom-search/call-endpoint-csharp
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 10/17/2020 | Initial build |~ 
    /// </revision>
    public class VideoPage
    {
        /// <value>string</value>
        public string name { get; set; }
        /// <value>DateTime</value>
        public DateTime datePublished { get; set; }
        /// <value>bool</value>
        public bool isFamilyFriendly { get; set; }
        /// <value>string</value>
        public string contentUrl { get; set; }
        /// <value>string</value>
        public string hostPageUrl { get; set; }
        /// <value>string</value>
        public string thumbnailUrl { get; set; }
        /// <value>string</value>
        public string encodingFormat { get; set; }
        /// <value>int</value>
        public int width { get; set; }
        /// <value>int</value>
        public int height { get; set; }
        /// <value>string</value>
        public string embedHtml { get; set; }
        /// <value>bool</value>
        public bool allowHttpsEmbed { get; set; }
        /// <value>thumbnail</value>
        public thumbnail thumbnail { get; set; }
        /// <value>string</value>
        public string videoId { get; set; }
        /// <value>bool</value>
        public bool allowMobileEmbed { get; set; }
        /// <value>bool</value>
        public bool isSuperfresh { get; set; }
        /// <value>string</value>
        public string description { get; set; }
    }
}
