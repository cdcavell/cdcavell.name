namespace CDCavell.ClassLibrary.Web.Mvc.Models.AppSettings
{
    /// <summary>
    /// reCAPTCHA Authentication model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.3 | 03/08/2021 | User Authorization Service |~ 
    /// </revision>
    public class reCAPTCHA
    {
        /// <value>string</value>
        public string SiteKey { get; set; }
        /// <value>string</value>
        public string SecretKey { get; set; }
    }
}
