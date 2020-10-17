using System.Net;

namespace cdcavell.Models.Search
{
    /// <summary>
    /// Bing Custom Search VideoResponse model
    /// &lt;br /&gt;&lt;br /&gt;
    /// https://docs.microsoft.com/en-us/azure/cognitive-services/bing-custom-search/call-endpoint-csharp
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 10/16/2020 | Initial build |~ 
    /// </revision>
    public class VideoResponse
    {
        /// <value>string</value>
        public string _type { get; set; }
        ///// <value>VideoPages</value>
        //public VideoPages webPages { get; set; }
        /// <value>HttpStatusCode</value>
        public HttpStatusCode StatusCode { get; set; }
        /// <value>string</value>
        public string StatusMessage { get; set; }
        /// <value>int</value>
        public int PageNumber { get; set; } = 1;
    }
}
