namespace dis5_cdcavell.Models.AppSettings
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
    /// | Christopher D. Cavell | 1.0.2.0 | 01/16/2021 | Initial build |~ 
    /// </revision>
    public class Facebook
    {
        /// <value>string</value>
        public string AppId { get; set; }
        /// <value>string</value>
        public string AppSecret { get; set; }
    }
}
