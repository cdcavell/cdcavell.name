﻿using as_ui_cdcavell.Filters;
using as_ui_cdcavell.Models.AppSettings;
using CDCavell.ClassLibrary.Web.Mvc.Controllers;
using CDCavell.ClassLibrary.Web.Mvc.Models;
using CDCavell.ClassLibrary.Web.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net;

namespace as_ui_cdcavell.Controllers
{
    /// <class>ApplicationBaseController</class>
    /// <summary>
    /// Base controller class for application
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 02/01/2021 | Initial build Authorization Service |~ 
    /// | Christopher D. Cavell | 1.0.3.1 | 02/07/2021 | Utilize Redis Cache |~ 
    /// | Christopher D. Cavell | 1.0.3.3 | 03/07/2021 | User Authorization Web Service |~ 
    /// </revision>
    [ServiceFilter(typeof(SecurityHeadersAttribute))]
    public abstract partial class ApplicationBaseController<T> : WebBaseController<ApplicationBaseController<T>> where T : ApplicationBaseController<T>
    {
        /// <value>AppSettings</value>
        protected AppSettings _appSettings;
        /// <value>AuthorizationUiDbContext</value>
        public AuthorizationDbContext _dbContext;
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
        /// <param name="dbContext">AuthorizationDbContext</param>
        /// <method>
        /// ApplicationBaseController(
        ///     ILogger&lt;T&gt; logger, 
        ///     IWebHostEnvironment webHostEnvironment, 
        ///     IHttpContextAccessor httpContextAccessor,
        ///     IAuthorizationService,
        ///     AppSettings appSettings,
        ///     AuthorizationDbContext dbContext
        /// )
        /// </method>
        protected ApplicationBaseController(
            ILogger<T> logger,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            AppSettings appSettings,
            AuthorizationDbContext dbContext
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
                case 7004:
                    vm.StatusMessage = "Error in saving information. ";
                    break;
                case 7005:
                    vm.StatusMessage = "Error in deleting information. ";
                    break;
            }

            return View("Error", vm);
        }
    }
}
