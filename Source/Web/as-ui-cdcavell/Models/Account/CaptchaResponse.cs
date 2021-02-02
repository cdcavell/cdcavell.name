using System;
using System.Collections.Generic;

namespace as_ui_cdcavell.Models.Account
{
    /// <summary>
    /// Captcha response model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/30/2021 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// </revision>
    public class CaptchaResponse
    {
        /// <value>bool</value>
        public bool success { get; set; }
        /// <value>DateTime</value>
        public DateTime challenge_ts { get; set; }
        /// <value>string</value>
        public string hostname { get; set; }
        /// <value>double</value>
        public double score { get; set; }
        /// <value>string</value>
        public string action { get; set; }
        /// <value>object[]</value>
        public object[] ErrorCodes { get; set; }
    }
}
