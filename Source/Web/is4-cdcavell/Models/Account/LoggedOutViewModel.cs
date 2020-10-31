namespace is4_cdcavell.Models.Account
{
    /// <summary>
    /// Logged Out View Model
    /// &lt;br /&gt;&lt;br /&gt;
    /// Copyright (c) Brock Allen &amp; Dominick Baier. All rights reserved.
    /// Licensed under the Apache License, Version 2.0. 
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 09/30/2020 | Initial build |~ 
    /// </revision>
    public class LoggedOutViewModel
    {
        /// <value>string</value>
        public string PostLogoutRedirectUri { get; set; }
        /// <value>string</value>
        public string ClientName { get; set; }
        /// <value>string</value>
        public string SignOutIframeUrl { get; set; }

        /// <value>bool</value>
        public bool AutomaticRedirectAfterSignOut { get; set; }

        /// <value>string</value>
        public string LogoutId { get; set; }
        /// <value>bool</value>
        public bool TriggerExternalSignout => ExternalAuthenticationScheme != null;
        /// <value>string</value>
        public string ExternalAuthenticationScheme { get; set; }
    }
}
