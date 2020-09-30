using System;

namespace is4_cdcavell.Controllers
{
    /// <summary>
    /// Account Options
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
    public class AccountOptions
    {
        /// <value>bool</value>
        public static bool AllowLocalLogin = true;
        /// <value>bool</value>
        public static bool AllowRememberLogin = true;
        /// <value>TimeSpan</value>
        public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);

        /// <value>bool</value>
        public static bool ShowLogoutPrompt = true;
        /// <value>bool</value>
        public static bool AutomaticRedirectAfterSignOut = false;

        /// <value>string</value>
        public static string InvalidCredentialsErrorMessage = "Invalid username or password";
    }
}
