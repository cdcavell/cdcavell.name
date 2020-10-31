namespace is4_cdcavell.Models.AppSettings
{
    /// <summary>
    /// Facebook Authentication model
    /// &lt;br/&gt;&lt;br/&gt;
    /// https://developers.facebook.com/apps/
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 10/10/2020 | Initial build |~ 
    /// </revision>
    public class Facebook
    {
        /// <value>string</value>
        public string AppId { get; set; }
        /// <value>string</value>
        public string AppSecret { get; set; }
    }
}
