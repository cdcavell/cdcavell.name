using Duende.IdentityServer;
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
    /// | Christopher D. Cavell | 1.0.2.2 | 01/18/2020 | Convert GranType from Implicit to Pkce |~ 
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
                new ApiScope("scope1"),
                new ApiScope("scope2")
            };

        /// <value>IEnumerable&lt;Client&gt;</value>
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "scope1" }
                },

                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { "https://localhost:44300/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "scope2"
                    }
                },

                // Token Access
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = new List<string>
                    {
                        GrantType.ClientCredentials
                    },
                    AllowedScopes = { "scope1" }
                },

                // OpenID Connect implicit flow client (MVC)
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    //AllowedGrantTypes = GrantTypes.Implicit,
                    
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,

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
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email
                    }
                }

            };
    }
}
