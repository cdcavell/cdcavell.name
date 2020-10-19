using Microsoft.AspNetCore.Authorization;

namespace cdcavell.Authorization
{
    /// <summary>
    /// Administration Requirement
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 10/18/2020 | Initial build |~ 
    /// </revision>
    public class AdministrationRequirement : IAuthorizationRequirement
    {
        /// <value>bool</value>
        public bool Administration { get; private set; }

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="isAdministration">bool</param>
        /// <method>AdministrationRequirement(bool isAdministration)</method>
        public AdministrationRequirement(bool isAdministration)
        {
            Administration = isAdministration;
        }
    }
}
