namespace CDCavell.ClassLibrary.Web.Mvc.Models.AppSettings
{
    /// <summary>
    /// IdP Model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.3 | 03/08/2021 | User Authorization Service |~ 
    /// </revision>
    public class IdP
    {
        /// <value>string</value>
        public string Authority { get; set; }
        /// <value>string</value>
        public string ClientId { get; set; }
        /// <value>string</value>
        public string ClientSecret { get; set; }
    }
}
