using Microsoft.AspNetCore.Authorization;

namespace CDCavell.ClassLibrary.Web.Services.Authorization
{
    /// <summary>
    /// User Requirement
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.3 | 03/07/2021 | User Authorization Service |~ 
    /// </revision>
    public class AuthenticatedRequirement : IAuthorizationRequirement
    {
        /// <value>bool</value>
        public bool Authenticated { get; private set; }

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="isAuthenticated">bool</param>
        /// <method>AuthenticatedRequirement(bool isAuthenticated)</method>
        public AuthenticatedRequirement(bool isAuthenticated)
        {
            Authenticated = isAuthenticated;
        }
    }
}
