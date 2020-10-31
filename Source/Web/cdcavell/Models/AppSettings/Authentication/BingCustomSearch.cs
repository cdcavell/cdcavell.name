namespace cdcavell.Models.AppSettings
{
    /// <summary>
    /// Bing Custom Search Authentication model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 10/10/2020 | Initial build |~ 
    /// </revision>
    public class BingCustomSearch
    {
        /// <value>string</value>
        public string SubscriptionKey { get; set; }
        /// <value>string</value>
        public string CustomConfigId { get; set; }
        /// <value>string</value>
        public string Url { get; set; }
    }
}
