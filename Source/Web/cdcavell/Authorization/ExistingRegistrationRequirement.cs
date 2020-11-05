using Microsoft.AspNetCore.Authorization;

namespace cdcavell.Authorization
{
    /// <summary>
    /// Existing Registration Requirement
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.9 | 11/04/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// </revision>
    public class ExistingRegistrationRequirement : IAuthorizationRequirement
    {
        /// <value>bool</value>
        public bool ExistingRegistration { get; private set; }

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="isExistingRegistration">bool</param>
        /// <method>ExistingRegistrationRequirement(bool isExistingRegistration)</method>
        public ExistingRegistrationRequirement(bool isExistingRegistration)
        {
            ExistingRegistration = isExistingRegistration;
        }
    }
}
