namespace CDCavell.ClassLibrary.Web.Mvc.Models.AppSettings
{
    /// <summary>
    /// Site Administrator model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.3 | 03/08/2021 | User Authorization Service |~ 
    /// </revision>
    public class SiteAdministrator
    {
        /// <value>string</value>
        public string Email { get; set; }
        /// <value>string</value>
        public string FirstName { get; set; }
        /// <value>string</value>
        public string LastName { get; set; }
    }
}
