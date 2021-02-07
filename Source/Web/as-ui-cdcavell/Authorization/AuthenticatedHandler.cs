using as_ui_cdcavell.Authorization;
using as_ui_cdcavell.Data;
using as_ui_cdcavell.Models.AppSettings;
using CDCavell.ClassLibrary.Web.Mvc.Models.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace as_ui_cdcavell.Authorization
{
    /// <summary>
    /// Authenticated Handler
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/31/2021 | Initial build Authorization Service |~ 
    /// | Christopher D. Cavell | 1.0.3.1 | 02/06/2021 | Utilize Redis Cache |~
    /// </revision>
    public class AuthenticatedHandler : AuthorizationHandler<AuthenticatedRequirement>
    {
        private AppSettings _appSettings;
        private AuthorizationUiDbContext _dbContext;
        private IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="appSettings">AppSettings</param>
        /// <param name="dbContext">AuthorizationUiDbContext</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        /// <method>AuthenticatedHandler(AppSettings appSettings, AuthorizationUiDbContext dbContext, IHttpContextAccessor httpContextAccessor)</method>
        public AuthenticatedHandler(AppSettings appSettings, AuthorizationUiDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _appSettings = appSettings;
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Handle Requirement method
        /// </summary>
        /// <param name="context">AuthorizationHandlerContext</param>
        /// <param name="requirement">AuthenticatedRequirement</param>
        /// <method>HandleRequirementAsync(AuthorizationHandlerContext context, AuthenticatedRequirement requirement)</method>
        /// <returns>Task</returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthenticatedRequirement requirement)
        {
            var user = context.User;
            if (user.Identity.IsAuthenticated)
            {
                Claim emailClaim = user.Claims.Where(x => x.Type == "email").FirstOrDefault();
                if (emailClaim != null)
                {
                    UserAuthorization userAuthorization = _httpContextAccessor.HttpContext.Session.Decrypt<UserAuthorization>("UserAuthorization").Result;
                    if (userAuthorization != null)
                        if (!string.IsNullOrEmpty(userAuthorization.Email))
                            if (userAuthorization.Email == emailClaim.Value)
                                context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
