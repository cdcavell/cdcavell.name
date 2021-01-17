namespace dis5_cdcavell.Models.AppSettings
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
    /// | Christopher D. Cavell | 1.0.2.0 | 01/16/2021 | Initial build |~ 
    /// </revision>
    public class LinkedIn
    {
        /// <value>string</value>
        public string ClientId { get; set; }
        /// <value>string</value>
        public string ClientSecret { get; set; }
    }
}
