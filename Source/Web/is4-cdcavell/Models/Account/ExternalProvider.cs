namespace is4_cdcavell.Models.Account
{
    /// <summary>
    /// External Provider Model
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
    public class ExternalProvider
    {
        /// <value>string</value>
        public string DisplayName { get; set; }
        /// <value>string</value>
        public string AuthenticationScheme { get; set; }
    }
}
