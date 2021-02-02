using Microsoft.AspNetCore.Authorization;

namespace as_api_cdcavell.Authorization
{
    /// <summary>
    /// Write Access Requirement
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/30/2021 | Initial build Authorization Service |~ 
    /// </revision>
    public class WriteRequirement : IAuthorizationRequirement
    {
        /// <value>bool</value>
        public bool Authorized { get; private set; }

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="isAuthorized">bool</param>
        /// <method>WriteRequirement(bool isAuthorized)</method>
        public WriteRequirement(bool isAuthorized)
        {
            Authorized = isAuthorized;
        }
    }
}
