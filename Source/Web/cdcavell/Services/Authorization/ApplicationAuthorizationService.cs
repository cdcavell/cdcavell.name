using cdcavell.Data;
using cdcavell.Models.AppSettings;
using CDCavell.ClassLibrary.Commons.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace cdcavell.Services.Authorization
{
    /// <summary>
    /// Application Authorization Web Service
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.1 | 02/09/2021 | User Authorization Web Service |~ 
    /// </revision>
    public class ApplicationAuthorizationService : IApplicationAuthorizationService
    {
        private Logger _logger;

        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">ILogger&lt;UserAuthorizationService&gt;</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        /// <param name="appSettings">AppSettings</param>
        /// <param name="dbContext">CDCavellDbContext</param>
        /// <param name="options">IOptions&lt;UserAuthorizationServiceOptionss&gt;</param>
        /// <method>ApplicationAuthorizationService(ILogger&lt;UserAuthorizationService&gt; logger, IHttpContextAccessor httpContextAccessor, IOptions&lt;ApplicationrAuthorizationServiceOptions&gt; options)</method>
        public ApplicationAuthorizationService(
            ILogger<ApplicationAuthorizationService> logger, 
            IHttpContextAccessor httpContextAccessor,
            AppSettings appSettings,
            CDCavellDbContext dbContext,
            IOptions<ApplicationAuthorizationServiceOptions> options
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = new Logger(logger);
        }

        public Task<List<Claim>> InitialAuthorization(TicketReceivedContext ticketReceivedContext)
        {
        }
    }
}
