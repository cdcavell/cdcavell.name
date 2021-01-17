using System.Collections.Generic;

namespace dis5_cdcavell.Models.Consent
{
    /// <summary>
    /// Consent Input Model
    /// &lt;br /&gt;&lt;br /&gt;
    /// Copyright (c) Duende Software. All rights reserved.
    /// See https://duendesoftware.com/license/identityserver.pdf for license information. 
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.2.0 | 01/16/2021 | Initial build |~ 
    /// </revision>
    public class ConsentInputModel
    {
        /// <value>string</value>
        public string Button { get; set; }
        /// <value>IEnumerable&lt;string&gt;</value>
        public IEnumerable<string> ScopesConsented { get; set; }
        /// <value>bool</value>
        public bool RememberConsent { get; set; }
        /// <value>string</value>
        public string ReturnUrl { get; set; }
        /// <value>string</value>
        public string Description { get; set; }
    }
}
