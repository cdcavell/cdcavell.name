using Microsoft.AspNetCore.Authorization;

namespace as_ui_cdcavell.Authorization
{
    /// <summary>
    /// User Requirement
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/30/2021 | Initial build Authorization Service |~ 
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
