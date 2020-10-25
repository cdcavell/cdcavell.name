﻿using System.Net;

namespace cdcavell.Models.Search.BingResult
{
    /// <summary>
    /// Bing Custom Search VideoPages model
    /// &lt;br /&gt;&lt;br /&gt;
    /// https://docs.microsoft.com/en-us/azure/cognitive-services/bing-custom-search/call-endpoint-csharp
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 10/23/2020 | Initial build |~ 
    /// </revision>
    public class VideoPages
    {
        /// <value>string</value>
        public string _type { get; set; }
        /// <value>string</value>
        public string webSearchUrl { get; set; }
        /// <value>int</value>
        public int totalEstimatedMatches { get; set; }
        /// <value>int</value>
        public int nextOffset { get; set; }
        /// <value>int</value>
        public int currentOffset { get; set; }
        /// <value>VideoPage[]</value>
        public VideoPage[] value { get; set; } = new VideoPage[0];
    }
}