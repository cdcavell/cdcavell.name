namespace dis5_cdcavell.Models.AppSettings
{
    /// <summary>
    /// Application model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.2.0 | 01/16/2021 | Initial build |~ 
    /// | Christopher D. Cavell | 1.0.3.0 | 02/02/2021 | Initial build Authorization Service |~ 
    /// </revision>
    public class Application
    {
        /// <value>string</value>
        public string SecretKey { get; set; }

        private string _mainSiteUrl;
        /// <value>string</value>
        public string MainSiteUrl { get; set; }

        /// <value>string</value>
        public string MainSiteUrlTrim
        {
            get { return this.MainSiteUrl.TrimEnd('/').TrimEnd('\\'); }
        }
    }
}
