using cdcavell.Data;
using cdcavell.Models.AppSettings;
using CDCavell.ClassLibrary.Web.Services.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace cdcavell.Authorization
{
    /// <summary>
    /// Authenticated Handler
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 10/18/2020 | Initial build |~ 
    /// | Christopher D. Cavell | 1.0.0.9 | 11/11/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// | Christopher D. Cavell | 1.0.3.0 | 02/01/2021 | Initial build Authorization Service |~ 
    /// | Christopher D. Cavell | 1.0.3.1 | 02/08/2021 | User Authorization Web Service |~ 
    /// </revision>
    public class AuthenticatedHandler : AuthorizationHandler<AuthenticatedRequirement>
    {
        private AppSettings _appSettings;
        private CDCavellDbContext _dbContext;
        private IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="appSettings">AppSettings</param>
        /// <param name="dbContext">CDCavellDbContext</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        /// <method>AuthenticatedHandler(AppSettings appSettings, CDCavellDbContext dbContext, IHttpContextAccessor httpContextAccessor)</method>
        public AuthenticatedHandler(AppSettings appSettings, CDCavellDbContext dbContext, IHttpContextAccessor httpContextAccessor)
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
                    UserAuthorizationModel userAuthorization = Data.Authorization.GetUser(user.Claims, _dbContext);
                    if (!string.IsNullOrEmpty(userAuthorization.Email))
                        if (userAuthorization.Email == emailClaim.Value)
                            context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
