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
    /// | Christopher D. Cavell | 1.0.0 | 10/18/2020 | Initial build |~ 
    /// </revision>
    public class UserRequirement : IAuthorizationRequirement
    {
        /// <value>bool</value>
        public bool User { get; private set; }

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="isUser">bool</param>
        /// <method>UserRequirement(bool isUser)</method>
        public UserRequirement(bool isUser)
        {
            User = isUser;
        }
    }
}
