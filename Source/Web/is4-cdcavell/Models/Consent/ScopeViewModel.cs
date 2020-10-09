namespace is4_cdcavell.Models.Consent
{
    /// <summary>
    /// Scope View Model
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
    public class ScopeViewModel
    {
        /// <value>string</value>
        public string Value { get; set; }
        /// <value>string</value>
        public string DisplayName { get; set; }
        /// <value>string</value>
        public string Description { get; set; }
        /// <value>bool</value>
        public bool Emphasize { get; set; }
        /// <value>bool</value>
        public bool Required { get; set; }
        /// <value>bool</value>
        public bool Checked { get; set; }
    }
}
