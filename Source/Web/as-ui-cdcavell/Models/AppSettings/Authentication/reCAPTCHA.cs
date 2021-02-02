namespace as_ui_cdcavell.Models.AppSettings
{
    /// <summary>
    /// reCAPTCHA Authentication model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/30/2021 | Initial build Authorization Service |~ 
    /// </revision>
    public class reCAPTCHA
    {
        /// <value>string</value>
        public string SiteKey { get; set; }
        /// <value>string</value>
        public string SecretKey { get; set; }
    }
}
