using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace is4_cdcavell.Models.Diagnostics
{
    /// <summary>
    /// Diagnostics View Model
    /// &lt;br /&gt;&lt;br /&gt;
    /// Copyright (c) Brock Allen &amp; Dominick Baier. All rights reserved.
    /// Licensed under the Apache License, Version 2.0. 
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 09/29/2020 | Initial build |~ 
    /// </revision>
    public class DiagnosticsViewModel
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="result">AuthenticateResult</param>
        public DiagnosticsViewModel(AuthenticateResult result)
        {
            AuthenticateResult = result;

            if (result.Properties.Items.ContainsKey("client_list"))
            {
                var encoded = result.Properties.Items["client_list"];
                var bytes = Base64Url.Decode(encoded);
                var value = Encoding.UTF8.GetString(bytes);

                Clients = JsonConvert.DeserializeObject<string[]>(value);
            }
        }

        /// <value>AuthenticateResult</value>
        public AuthenticateResult AuthenticateResult { get; }
        /// <value>Clients</value>
        public IEnumerable<string> Clients { get; } = new List<string>();
    }
}
