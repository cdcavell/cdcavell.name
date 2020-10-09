using System.ComponentModel.DataAnnotations;

namespace is4_cdcavell.Models.Account
{
    /// <summary>
    /// Login Input Model
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
    public class LoginInputModel
    {
        /// <value>string</value>
        [Required]
        public string Username { get; set; }
        /// <value>string</value>
        [Required]
        public string Password { get; set; }
        /// <value>bool</value>
        public bool RememberLogin { get; set; }
        /// <value>string</value>
        public string ReturnUrl { get; set; }
    }
}
