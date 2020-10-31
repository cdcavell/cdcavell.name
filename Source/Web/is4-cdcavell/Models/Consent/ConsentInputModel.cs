using System.Collections.Generic;

namespace is4_cdcavell.Models.Consent
{
    /// <summary>
    /// Consent Input Model
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
