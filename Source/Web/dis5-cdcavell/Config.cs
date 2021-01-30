﻿using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using System.Collections.Generic;

namespace dis5_cdcavell
{
    /// <summary>
    /// IdentityServer4 In Memory Configuration
    /// &lt;br /&gt;&lt;br /&gt;
    /// Copyright (c) Duende Software. All rights reserved.
    /// See https://duendesoftware.com/license/identityserver.pdf for license information. 
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.2.0 | 01/16/2020 | Initial build |~ 
    /// | Christopher D. Cavell | 1.0.2.2 | 01/18/2020 | Convert GrantType from Implicit to Pkce |~ 
    /// | Christopher D. Cavell | 1.0.2.2 | 01/18/2020 | Removed unused clients and scopes |~ 
    /// | Christopher D. Cavell | 1.0.3.0 | 01/30/2020 | Initial build Authorization Service |~ 
    /// </revision>
    public static class Config
    {
        /// <value>IEnumerable&lt;IdentityResource&gt;</value>
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };

        /// <value>IEnumerable&lt;ApiScope&gt;</value>
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("Authorization.Service.API.Read"),
                new ApiScope("Authorization.Service.API.Write")
            };

        /// <value>IEnumerable&lt;Client&gt;</value>
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "Authorization.Service.UI",
                    ClientName = "Authorization Service UI",

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    AllowOfflineAccess = true,

                    // where to redirect to after login
                    RedirectUris = new List<string>
                    {
                        "https://localhost:44305/signin-oidc",
                        "https://as-ui-cdcavell.azurewebsites.net/signin-oidc"
                    },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://localhost:44305/signout-callback-oidc",
                        "https://as-ui-cdcavell.azurewebsites.net/signout-callback-oidc"
                    },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        "Authorization.Service.API.Read",
                        "Authorization.Service.API.Write"
                    }
                },
                // OpenID Connect interactive client using code flow + pkce (MVC)
                new Client
                {
                    ClientId = "cdcavell.name",
                    ClientName = "Personal Website of Christopher D. Cavell",
                    
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    AllowOfflineAccess = true,

                    // where to redirect to after login
                    RedirectUris = new List<string>
                    { 
                        "https://localhost:44349/signin-oidc",
                        "https://cdcavell.name/signin-oidc"
                    },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = new List<string>
                    { 
                        "https://localhost:44349/signout-callback-oidc",
                        "https://cdcavell.name/signout-callback-oidc"
                    },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        "Authorization.Service.API.Read"
                    },

                    AlwaysIncludeUserClaimsInIdToken = true
                }

            };
    }
}
