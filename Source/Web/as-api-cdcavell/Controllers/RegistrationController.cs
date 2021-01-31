using as_api_cdcavell.Data;
using as_api_cdcavell.Models.AppSettings;
using CDCavell.ClassLibrary.Web.Mvc.Models.Authorization;
using CDCavell.ClassLibrary.Web.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace as_api_cdcavell.Controllers
{
    /// <class>AuthorizationController</class>
    /// <summary>
    /// User authorization controller class
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/30/2021 | Initial build Authorization Service |~ 
    /// </revision>
    public class RegistrationController : ApplicationBaseController<RegistrationController>
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="logger">ILogger&lt;RegistrationController&gt;</param>
        /// <param name="webHostEnvironment">IWebHostEnvironment</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        /// <param name="authorizationService">IAuthorizationService</param>
        /// <param name="appSettings">AppSettings</param>
        /// <param name="dbContext">AuthorizationServiceDbContext</param>
        /// <method>
        /// public RegistrationController(
        ///     ILogger&lt;RegistrationController&gt; logger,
        ///     IWebHostEnvironment webHostEnvironment,
        ///     IHttpContextAccessor httpContextAccessor,
        ///     IAuthorizationService authorizationService,
        ///     AppSettings appSettings,
        ///     AuthorizationServiceDbContext dbContext
        /// ) : base(logger, webHostEnvironment, httpContextAccessor, authorizationService, appSettings, dbContext)
        /// </method>
        public RegistrationController(
            ILogger<RegistrationController> logger,
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
        [Authorize(Policy = "Authenticated")]
        public IActionResult Get()
        {
            IHeaderDictionary headers = _httpContextAccessor.HttpContext.Request.Headers;
            string accessToken = headers.Where(x => x.Key == "Authorization").Select(x => x.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(accessToken))
                return BadRequest("Invalid access token");

            accessToken = accessToken.Substring(7);

            RegistrationCheck registrationCheck = new RegistrationCheck();
            registrationCheck.IsRegistered = false;
            registrationCheck.Email = string.Empty;


            string  email = User.Claims.Where(x => x.Type == "email").Select(x => x.Value).FirstOrDefault();
            if (!string.IsNullOrEmpty(email))
            {
                registrationCheck.Email = email;
                Data.Registration registration = Data.Registration.Get(email, _dbContext);
                registrationCheck.IsRegistered = registration.IsRegistered;
            }

            string jsonString = JsonConvert.SerializeObject(registrationCheck);
            string encryptString = AESGCM.Encrypt(jsonString, accessToken);
            return new JsonResult(encryptString);
        }
    }
}
