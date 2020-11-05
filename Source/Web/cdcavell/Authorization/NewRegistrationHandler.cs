using cdcavell.Models.AppSettings;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace cdcavell.Authorization
{
    /// <summary>
    /// New Registration Handler
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.9 | 11/04/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// </revision>
    public class NewRegistrationHandler : AuthorizationHandler<NewRegistrationRequirement>
    {
        private AppSettings _appSettings;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="appSettings">AppSettings</param>
        /// <method>NewRegistrationHandler(AppSettings appSettings)</method>
        public NewRegistrationHandler(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        /// <summary>
        /// Handle Requirement method
        /// </summary>
        /// <param name="context">AuthorizationHandlerContext</param>
        /// <param name="requirement">NewRegistrationRequirement</param>
        /// <method>HandleRequirementAsync(AuthorizationHandlerContext context, NewRegistrationRequirement requirement)</method>
        /// <returns>Task</returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, NewRegistrationRequirement requirement)
        {
            var user = context.User;
            if (user.Identity.IsAuthenticated)
            {
                List<Claim> registrationClaims = user.Claims.Where(x => x.Type == "registration").ToList();
                if (registrationClaims != null)
                {
                    Claim newClaim = registrationClaims.Where(x => x.Value.Trim()
                        .Contains("new", StringComparison.CurrentCultureIgnoreCase))
                        .FirstOrDefault();

                    Claim existingClaim = registrationClaims.Where(x => x.Value.Trim()
                        .Contains("existing", StringComparison.CurrentCultureIgnoreCase))
                        .FirstOrDefault();

                    if (newClaim != null && existingClaim == null)
                    {
                        context.Succeed(requirement);
                    }

                }
            }

            return Task.CompletedTask;
        }
    }
}
