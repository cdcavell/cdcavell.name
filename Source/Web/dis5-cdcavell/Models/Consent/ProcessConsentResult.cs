using Duende.IdentityServer.Models;

namespace dis5_cdcavell.Models.Consent
{
    /// <summary>
    /// Process Consent Result Model
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
    public class ProcessConsentResult
    {
        /// <value>bool</value>
        public bool IsRedirect => RedirectUri != null;
        /// <value>string</value>
        public string RedirectUri { get; set; }
        /// <value>Client</value>
        public Client Client { get; set; }

        /// <value>bool</value>
        public bool ShowView => ViewModel != null;
        /// <value>ConsentViewModel</value>
        public ConsentViewModel ViewModel { get; set; }

        /// <value>bool</value>
        public bool HasValidationError => ValidationError != null;
        /// <value>string</value>
        public string ValidationError { get; set; }
    }
}
