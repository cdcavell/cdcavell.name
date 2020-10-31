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
    /// User Handler
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 10/18/2020 | Initial build |~ 
    /// </revision>
    public class UserHandler : AuthorizationHandler<UserRequirement>
    {
        private AppSettings _appSettings;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="appSettings">AppSettings</param>
        /// <method>AdministrationHandler(AppSettings appSettings)</method>
        public UserHandler(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        /// <summary>
        /// Handle Requirement method
        /// </summary>
        /// <param name="context">AuthorizationHandlerContext</param>
        /// <param name="requirement">UserRequirement</param>
        /// <method>HandleRequirementAsync(AuthorizationHandlerContext context, UserRequirement requirement)</method>
        /// <returns>Task</returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRequirement requirement)
        {
            var user = context.User;
            if (user.Identity.IsAuthenticated)
            {
                List<Claim> roles = user.Claims.Where(x => x.Type == "email").ToList();
                if (roles != null && roles.Count > 0)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
