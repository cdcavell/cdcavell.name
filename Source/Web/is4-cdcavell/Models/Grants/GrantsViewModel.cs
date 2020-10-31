using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace is4_cdcavell.Models.Grants
{
    /// <summary>
    /// Grants View Model
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
    public class GrantsViewModel
    {
        /// <value>IEnumerable&lt;GrantViewModel&gt;</value>
        public IEnumerable<GrantViewModel> Grants { get; set; }
    }

    /// <summary>
    /// Grant View Model
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
    public class GrantViewModel
    {
        /// <value>string</value>
        public string ClientId { get; set; }
        /// <value>string</value>
        public string ClientName { get; set; }
        /// <value>string</value>
        public string ClientUrl { get; set; }
        /// <value>string</value>
        public string ClientLogoUrl { get; set; }
        /// <value>string</value>
        public string Description { get; set; }
        /// <value>DateTime</value>
        public DateTime Created { get; set; }
        /// <value>DateTime?</value>
        public DateTime? Expires { get; set; }
        /// <value>IEnumerable&lt;string&gt;</value>
        public IEnumerable<string> IdentityGrantNames { get; set; }
        /// <value>IEnumerable&lt;string&gt;</value>
        public IEnumerable<string> ApiGrantNames { get; set; }
    }
}
