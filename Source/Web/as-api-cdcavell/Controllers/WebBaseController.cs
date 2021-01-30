using as_api_cdcavell.Data;
using as_api_cdcavell.Filters;
using as_api_cdcavell.Models.AppSettings;
using CDCavell.ClassLibrary.Web.Mvc.Controllers;
using CDCavell.ClassLibrary.Web.Mvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net;

namespace as_api_cdcavell.Controllers
{
    /// <class>ApplicationWebBaseController</class>
    /// <summary>
    /// Web base controller class for web application
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/30/2020 | Initial build Authorization Service |~ 
    /// </revision>
    [ServiceFilter(typeof(SecurityHeadersAttribute))]
    public class ApplicationWebBaseController<T> : WebBaseController<ApplicationWebBaseController<T>> where T : ApplicationWebBaseController<T>
    {
        /// <value>AppSettings</value>
        protected AppSettings _appSettings;
        /// <value>CDCavellDbContext</value>
        public AuthorizationServiceDbContext _dbContext;
        /// <value>IAuthorizationService</value>
        public IAuthorizationService _authorizationService;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="logger">ILogger</param>
        /// <param name="webHostEnvironment">IWebHostEnvironment</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        /// <param name="authorizationService">IAuthorizationService</param>
        /// <param name="appSettings">AppSettings</param>
        /// <param name="dbContext">CDCavellDbContext</param>
        /// <method>
        /// ApplicationBaseController(
        ///     ILogger&lt;T&gt; logger, 
        ///     IWebHostEnvironment webHostEnvironment, 
        ///     IHttpContextAccessor httpContextAccessor,
        ///     IAuthorizationService,
        ///     AppSettings appSettings,
        ///     CDCavellDbContext dbContext
        /// )
        /// </method>
        protected ApplicationWebBaseController(
            ILogger<T> logger,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            AppSettings appSettings,
            AuthorizationServiceDbContext dbContext
        ) : base(logger, webHostEnvironment, httpContextAccessor)
        {
            _appSettings = appSettings;
            _dbContext = dbContext;
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// Global error handling
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>IActionResult</returns>
        [AllowAnonymous]
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public override IActionResult Error(int id)
        {
            if (id < 7000)
                return base.Error(id);

            var vm = new ErrorViewModel(id);
            vm.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            vm.StatusCode = (int)HttpStatusCode.Unauthorized;

            switch (id)
            {
                case 7001:
                    vm.StatusMessage = "An invalid access token was received. ";
                    break;
                case 7002:
                    vm.StatusMessage = "Unable to access Authorization Service. ";
                    break;
                case 7003:
                    vm.StatusMessage = "Invalid or missing email returned. ";
                    break;
            }

            return View("Error", vm);
        }
    }
}
