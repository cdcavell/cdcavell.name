using System.ComponentModel.DataAnnotations;

namespace dis5_cdcavell.Models.Account
{
    /// <summary>
    /// Login Input Model
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
