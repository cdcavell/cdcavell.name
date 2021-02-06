using System;

namespace dis5_cdcavell.Options
{
    /// <summary>
    /// Account Options
    /// &lt;br /&gt;&lt;br /&gt;
    /// Copyright (c) Duende Software. All rights reserved.
    /// See https://duendesoftware.com/license/identityserver.pdf for license information. 
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.2.0 | 01/16/2021 | Initial build |~ 
    /// | Christopher D. Cavell | 1.0.3.0 | 02/06/2021 | Initial build Authorization Service |~ 
    /// </revision>
    public class AccountOptions
    {
        /// <value>bool</value>
        public static bool AllowLocalLogin = false;
        /// <value>bool</value>
        public static bool AllowRememberLogin = true;
        /// <value>TimeSpan</value>
        public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);

        /// <value>bool</value>
        public static bool ShowLogoutPrompt = false;
        /// <value>bool</value>
        public static bool AutomaticRedirectAfterSignOut = true;

        /// <value>string</value>
        public static string InvalidCredentialsErrorMessage = "Invalid username or password";
    }
}
