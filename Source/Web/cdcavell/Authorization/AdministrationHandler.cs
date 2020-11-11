
using cdcavell.Data;
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
    /// Administration Handler
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 10/18/2020 | Initial build |~ 
    /// | Christopher D. Cavell | 1.0.0.9 | 11/11/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// </revision>
    public class AdministrationHandler : AuthorizationHandler<AdministrationRequirement>
    {
        private AppSettings _appSettings;
        private CDCavellDbContext _dbContext;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="appSettings">AppSettings</param>
        /// <param name="dbContext">CDCavellDbContext</param>
        /// <method>AdministrationHandler(AppSettings appSettings, CDCavellDbContext dbContext)</method>
        public AdministrationHandler(AppSettings appSettings, CDCavellDbContext dbContext)
        {
            _appSettings = appSettings;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Handle Requirement method
        /// </summary>
        /// <param name="context">AuthorizationHandlerContext</param>
        /// <param name="requirement">AdministrationRequirement</param>
        /// <method>HandleRequirementAsync(AuthorizationHandlerContext context, AdministrationRequirement requirement)</method>
        /// <returns>Task</returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdministrationRequirement requirement)
        {
            var user = context.User;
            if (user.Identity.IsAuthenticated)
            {
                List<Claim> roles = user.Claims.Where(x => x.Type == "email").ToList();
                if (roles != null && roles.Count > 0)
                {
                    Claim rolePermission = roles.Where(x => x.Value.Trim()
                        .Contains(_appSettings.Authorization.AdministratorEmail, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                    if (rolePermission != null)
                        context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
