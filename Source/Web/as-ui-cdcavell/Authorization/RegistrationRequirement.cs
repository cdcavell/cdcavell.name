using Microsoft.AspNetCore.Authorization;

namespace as_ui_cdcavell.Authorization
{
    /// <summary>
    /// Registration Requirement
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/30/2021 | Initial build Authorization Service |~ 
    /// </revision>
    public class RegistrationRequirement : IAuthorizationRequirement
    {
        /// <value>bool</value>
        public bool Authenticated { get; private set; }

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="isAuthenticated">bool</param>
        /// <method>RegistrationRequirement(bool isAuthenticated)</method>
        public RegistrationRequirement(bool isAuthenticated)
        {
            Authenticated = isAuthenticated;
        }
    }
}
