﻿namespace as_api_cdcavell.Models.AppSettings
{
    /// <summary>
    /// Application model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/18/2021 | Initial build Authorization Service |~ 
    /// | Christopher D. Cavell | 1.0.3.3 | 02/27/2021 | User Authorization Service |~ 
    /// </revision>
    public class Application
    {
        /// <value>string</value>
        public string SecretKey { get; set; }
        /// <value>SiteAdministrator</value>
        public SiteAdministrator SiteAdministrator { get; set; }
    }
}
