﻿using System;

namespace cdcavell.Models.Home.Search.BingResult
{
    /// <summary>
    /// Bing Custom Search SearchPage model
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
    public class SearchPage
    {
        /// <value>string</value>
        public string name { get; set; }
        /// <value>string</value>
        public string url { get; set; }
        /// <value>string</value>
        public string displayUrl { get; set; }
        /// <value>string</value>
        public string snippet { get; set; }
        /// <value>DateTime</value>
        public DateTime dateLastCrawled { get; set; }
        /// <value>string</value>
        public string cachedPageUrl { get; set; }
    }
}
