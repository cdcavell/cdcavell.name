namespace as_api_cdcavell.Models.AppSettings
{
    /// <summary>
    /// Authentication model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/30/2021 | Initial build Authorization Service |~ 
    /// </revision>
    public class Authentication
    {
        /// <value>IdP</value>
        public IdP IdP { get; set; }
        /// <value>reCAPTCHA</value>
        public reCAPTCHA reCAPTCHA { get; set; }
    }
}
