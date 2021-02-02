using as_api_cdcavell.Data;
using as_api_cdcavell.Models.AppSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace as_api_cdcavell.Controllers
{
    /// <class>DateTimeController</class>
    /// <summary>
    /// Current date time controller class for application
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/19/2021 | Initial build Authorization Service |~ 
    /// </revision>
    [AllowAnonymous]
    public class DateTimeController : ApplicationBaseController<DateTimeController>
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="logger">ILogger</param>
        /// <param name="webHostEnvironment">IWebHostEnvironment</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        /// <param name="authorizationService">IAuthorizationService</param>
        /// <param name="appSettings">AppSettings</param>
        /// <param name="dbContext">AuthorizationServiceDbContext</param>
        /// <method>
        /// public DateTimeController(
        ///     ILogger&lt;DateTimeController&gt; logger,
        ///     IWebHostEnvironment webHostEnvironment,
        ///     IHttpContextAccessor httpContextAccessor,
        ///     IAuthorizationService authorizationService,
        ///     AppSettings appSettings,
        ///     AuthorizationServiceDbContext dbContext
        /// ) : base(logger, webHostEnvironment, httpContextAccessor, authorizationService, appSettings, dbContext)
        /// </method>
        public DateTimeController(
            ILogger<DateTimeController> logger,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            AppSettings appSettings,
            AuthorizationServiceDbContext dbContext
        ) : base(logger, webHostEnvironment, httpContextAccessor, authorizationService, appSettings, dbContext)
        {
        }

        /// <summary>
        /// Get action method
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            var currentDateTime = new
            {
                System = DateTime.Now.ToString(),
                UTC = DateTime.UtcNow.ToString()
            };
            return new JsonResult(currentDateTime);
        }
    }
}
