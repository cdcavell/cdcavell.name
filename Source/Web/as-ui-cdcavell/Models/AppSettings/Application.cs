namespace as_ui_cdcavell.Models.AppSettings
{
    /// <summary>
    /// Application model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 02/02/2021 | Initial build Authorization Service |~ 
    /// </revision>
    public class Application
    {
        /// <value>string</value>
        public string SecretKey { get; set; }
        /// <value>string</value>
        public string MainSiteUrl { get; set; }

        /// <value>string</value>
        public string MainSiteUrlTrim {
            get { return this.MainSiteUrl.TrimEnd('/').TrimEnd('\\'); }
        }
    }
}
