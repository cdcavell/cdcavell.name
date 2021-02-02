namespace cdcavell.Models.AppSettings
{
    /// <summary>
    /// Authorization model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 10/18/2020 | Initial build |~ 
    /// | Christopher D. Cavell | 1.0.3.0 | 01/18/2021 | Initial build Authorization Service |~ 
    /// </revision>
    public class Authorization
    {
        /// <value>string</value>
        public string AdministratorEmail { get; set; }
        /// <value>AuthorizationService</value>
        public AuthorizationService AuthorizationService { get; set; }
    }
}
