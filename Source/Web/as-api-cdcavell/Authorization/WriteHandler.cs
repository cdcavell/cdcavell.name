using as_api_cdcavell.Data;
using as_api_cdcavell.Models.AppSettings;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace as_api_cdcavell.Authorization
{
    /// <summary>
    /// Write Handler
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/30/2020 | Initial build Authorization Service |~ 
    /// </revision>
    public class WriteHandler : AuthorizationHandler<WriteRequirement>
    {
        private AppSettings _appSettings;
        private AuthorizationServiceDbContext _dbContext;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="appSettings">AppSettings</param>
        /// <param name="dbContext">AuthorizationServiceDbContext</param>
        /// <method>WriteHandler(AppSettings appSettings, AuthorizationServiceDbContext dbContext)</method>
        public WriteHandler(AppSettings appSettings, AuthorizationServiceDbContext dbContext)
        {
            _appSettings = appSettings;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Handle Requirement method
        /// </summary>
        /// <param name="context">AuthorizationHandlerContext</param>
        /// <param name="requirement">AuthenticatedRequirement</param>
        /// <method>HandleRequirementAsync(AuthorizationHandlerContext context, WriteRequirement requirement)</method>
        /// <returns>Task</returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, WriteRequirement requirement)
        {
            var user = context.User;
            if (user.Identity.IsAuthenticated)
            {
                Claim emailClaim = user.Claims.Where(x => x.Type == "email").FirstOrDefault();
                if (emailClaim != null)
                {
                    as_api_cdcavell.Data.Registration registration = as_api_cdcavell.Data.Registration
                        .Get(emailClaim.Value, _dbContext);
                    if (registration.IsActive)
                    {
                        Claim readClaim = user.Claims.Where(x => x.Type == "scope")
                            .Where(x => x.Value == "Authorization.Service.API.Write")
                            .FirstOrDefault();

                        if (readClaim != null)
                            context.Succeed(requirement);
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
