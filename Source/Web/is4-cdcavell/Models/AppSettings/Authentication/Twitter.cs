namespace is4_cdcavell.Models.AppSettings
{
    /// <summary>
    /// Twitter Authentication model
    /// &lt;br/&gt;&lt;br/&gt;
    /// https://developer.twitter.com/en/portal/dashboard
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 10/10/2020 | Initial build |~ 
    /// </revision>
    public class Twitter
    {
        /// <value>string</value>
        public string ConsumerAPIKey { get; set;}
        /// <value>string</value>
        public string ConsumerSecret { get; set; }
        /// <value>string</value>
        public string BearerToken { get; set; }
    }
}
