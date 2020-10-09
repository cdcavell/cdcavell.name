using System.Collections.Generic;

namespace is4_cdcavell.Models.Consent
{
    /// <summary>
    /// Consent View Model
    /// &lt;br /&gt;&lt;br /&gt;
    /// Copyright (c) Brock Allen &amp; Dominick Baier. All rights reserved.
    /// Licensed under the Apache License, Version 2.0. 
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 09/30/2020 | Initial build |~ 
    /// </revision>
    public class ConsentViewModel : ConsentInputModel
    {
        /// <value>string</value>
        public string ClientName { get; set; }
        /// <value>string</value>
        public string ClientUrl { get; set; }
        /// <value>string</value>
        public string ClientLogoUrl { get; set; }
        /// <value>bool</value>
        public bool AllowRememberConsent { get; set; }

        /// <value>IEnumerable&lt;ScopeViewModel&gt;</value>
        public IEnumerable<ScopeViewModel> IdentityScopes { get; set; }
        /// <value>IEnumerable&lt;ScopeViewModel&gt;</value>
        public IEnumerable<ScopeViewModel> ApiScopes { get; set; }
    }
}
