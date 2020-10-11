namespace cdcavell.Models.Search
{
    /// <summary>
    /// Bing Custom Search Response model
    /// &lt;br /&gt;&lt;br /&gt;
    /// https://docs.microsoft.com/en-us/azure/cognitive-services/bing-custom-search/call-endpoint-csharp
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 10/10/2020 | Initial build |~ 
    /// </revision>
    public class SearchResponse
    {
        /// <value>string</value>
        public string _type { get; set; }
        /// <value>WebPages</value>
        public WebPages webPages { get; set; }
    }
}
