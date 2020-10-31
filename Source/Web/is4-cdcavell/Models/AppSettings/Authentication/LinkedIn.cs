namespace is4_cdcavell.Models.AppSettings
{
    /// <summary>
    /// LinkedIn Authentication model
    /// &lt;br/&gt;&lt;br/&gt;
    /// https://www.linkedin.com/developers/apps
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 10/10/2020 | Initial build |~ 
    /// </revision>
    public class LinkedIn
    {
        /// <value>string</value>
        public string ClientId { get; set; }
        /// <value>string</value>
        public string ClientSecret { get; set; }
    }
}
