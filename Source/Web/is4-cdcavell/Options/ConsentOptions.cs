using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace is4_cdcavell.Options
{
    /// <summary>
    /// Consent Options
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
    public class ConsentOptions
    {
        /// <value>bool</value>
        public static bool EnableOfflineAccess = true;
        /// <value>string</value>
        public static string OfflineAccessDisplayName = "Offline Access";
        /// <value>string</value>
        public static string OfflineAccessDescription = "Access to your applications and resources, even when you are offline";

        /// <value>string</value>
        public static readonly string MustChooseOneErrorMessage = "You must pick at least one permission";
        /// <value>string</value>
        public static readonly string InvalidSelectionErrorMessage = "Invalid selection";
    }
}
