﻿namespace cdcavell.Models.Home.Search.BingResult
{
    /// <summary>
    /// Bing Custom Search SearchResponse model
    /// &lt;br /&gt;&lt;br /&gt;
    /// https://docs.microsoft.com/en-us/azure/cognitive-services/bing-custom-search/call-endpoint-csharp
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 10/25/2020 | Initial build |~ 
    /// | Christopher D. Cavell | 1.0.0.1 | 10/28/2020 | Update namespace |~ 
    /// </revision>
    public class SearchResponse
    {
        /// <value>string</value>
        public string _type { get; set; }
        /// <value>SearchPages</value>
        public SearchPages webPages { get; set; }
    }
}
