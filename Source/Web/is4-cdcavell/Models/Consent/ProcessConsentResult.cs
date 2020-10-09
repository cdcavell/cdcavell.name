using IdentityServer4.Models;

namespace is4_cdcavell.Models.Consent
{
    /// <summary>
    /// Process Consent Result Model
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
