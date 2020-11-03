using Microsoft.AspNetCore.Authorization;

namespace cdcavell.Authorization
{
    /// <summary>
    /// User Requirement
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 10/18/2020 | Initial build |~ 
    /// | Christopher D. Cavell | 1.0.0.9 | 11/03/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// </revision>
    public class AuthenticatedRequirement : IAuthorizationRequirement
    {
        /// <value>bool</value>
        public bool Authenticated { get; private set; }

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="isAuthenticated">bool</param>
        /// <method>UserRequirement(bool isAuthenticated)</method>
        public AuthenticatedRequirement(bool isAuthenticated)
        {
            Authenticated = isAuthenticated;
        }
    }
}
