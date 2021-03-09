namespace CDCavell.ClassLibrary.Web.Mvc.Models.AppSettings
{
    /// <summary>
    /// Authentication model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.3 | 03/08/2021 | User Authorization Service |~ 
    /// </revision>
    public class Authentication
    {
        /// <value>IdP</value>
        public IdP IdP { get; set; }
        /// <value>reCAPTCHA</value>
        public reCAPTCHA reCAPTCHA { get; set; }
    }
}
