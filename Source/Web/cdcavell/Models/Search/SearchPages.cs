namespace cdcavell.Models.Search
{
    /// <summary>
    /// Bing Custom Search SearchPages model
    /// &lt;br /&gt;&lt;br /&gt;
    /// https://docs.microsoft.com/en-us/azure/cognitive-services/bing-custom-search/call-endpoint-csharp
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 10/12/2020 | Initial build |~ 
    /// </revision>
    public class SearchPages
    {
        /// <value>string</value>
        public string webSearchUrl { get; set; }
        /// <value>int</value>
        public int totalEstimatedMatches { get; set; }
        /// <value>WebPage[]</value>
        public SearchPage[] value { get; set; }
    }
}
