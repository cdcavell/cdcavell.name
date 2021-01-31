using as_ui_cdcavell.Data;
using as_ui_cdcavell.Models.AppSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace as_ui_cdcavell.Authorization
{
    /// <summary>
    /// Registration Handler
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/30/2021 | Initial build Authorization Service |~ 
    /// </revision>
    public class RegistrationHandler : AuthorizationHandler<RegistrationRequirement>
    {
        private AppSettings _appSettings;
        private IHttpContextAccessor _httpContextAccessor;
        private AuthorizationUiDbContext _dbContext;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="appSettings">AppSettings</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        /// <param name="dbContext">AuthorizationUiDbContext</param>
        /// <method>RegistrationHandler(AppSettings appSettings, AuthorizationUiDbContext dbContext)</method>
        public RegistrationHandler(AppSettings appSettings, IHttpContextAccessor httpContextAccessor, AuthorizationUiDbContext dbContext)
        {
            _appSettings = appSettings;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Handle Requirement method
        /// </summary>
        /// <param name="context">AuthorizationHandlerContext</param>
        /// <param name="requirement">AuthenticatedRequirement</param>
        /// <method>HandleRequirementAsync(AuthorizationHandlerContext context, RegistrationRequirement requirement)</method>
        /// <returns>Task</returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RegistrationRequirement requirement)
        {
            HttpRequest request = _httpContextAccessor.HttpContext.Request;
            IFormCollection form = request.Form;

            string email = form.Where(x => x.Key == "email").Select(x => x.Value).FirstOrDefault();
            if (!string.IsNullOrEmpty(email))
            {
                List<IAuthorizationRequirement> pendingRequirements = context.PendingRequirements.ToList();
                foreach (IAuthorizationRequirement authorizationRequirement in pendingRequirements)
                    context.Succeed(authorizationRequirement);
            }

            return Task.CompletedTask;
        }
    }
}
