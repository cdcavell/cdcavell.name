using System;
using System.Collections.Generic;
using System.Linq;

namespace is4_cdcavell.Models.Account
{
    /// <summary>
    /// Login View Model
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
    public class LoginViewModel : LoginInputModel
    {
        /// <value>bool</value>
        public bool AllowRememberLogin { get; set; } = true;
        /// <value>bool</value>
        public bool EnableLocalLogin { get; set; } = true;

        /// <value>IEnumerable&lt;ExternalProvider&gt;</value>
        public IEnumerable<ExternalProvider> ExternalProviders { get; set; } = Enumerable.Empty<ExternalProvider>();
        /// <value>IEnumerable&lt;ExternalProvider&gt;</value>
        public IEnumerable<ExternalProvider> VisibleExternalProviders => ExternalProviders.Where(x => !String.IsNullOrWhiteSpace(x.DisplayName));

        /// <value>bool</value>
        public bool IsExternalLoginOnly => EnableLocalLogin == false && ExternalProviders?.Count() == 1;
        /// <value>string</value>
        public string ExternalLoginScheme => IsExternalLoginOnly ? ExternalProviders?.SingleOrDefault()?.AuthenticationScheme : null;
    }
}
