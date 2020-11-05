using Microsoft.AspNetCore.Authorization;

namespace cdcavell.Authorization
{
    /// <summary>
    /// New Registration Requirement
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.9 | 11/04/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// </revision>
    public class NewRegistrationRequirement : IAuthorizationRequirement
    {
        /// <value>bool</value>
        public bool NewRegistration { get; private set; }

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="isNewRegistration">bool</param>
        /// <method>NewRegistrationRequirement(bool isNewRegistration)</method>
        public NewRegistrationRequirement(bool isNewRegistration)
        {
            NewRegistration = isNewRegistration;
        }
    }
}
