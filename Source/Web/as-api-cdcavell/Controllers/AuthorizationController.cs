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
using System.Linq;
using System.Text;

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
    /// | Christopher D. Cavell | 1.0.3.0 | 02/01/2021 | Initial build Authorization Service |~ 
    /// </revision>
    public class AuthorizationController : ApplicationBaseController<AuthorizationController>
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
        /// public AuthorizationController(
        ///     ILogger&lt;AuthorizationController&gt; logger,
        ///     IWebHostEnvironment webHostEnvironment,
        ///     IHttpContextAccessor httpContextAccessor,
        ///     IAuthorizationService authorizationService,
        ///     AppSettings appSettings,
        ///     AuthorizationServiceDbContext dbContext
        /// ) : base(logger, webHostEnvironment, httpContextAccessor, authorizationService, appSettings, dbContext)
        /// </method>
        public AuthorizationController(
            ILogger<AuthorizationController> logger,
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
        [Authorize(Policy = "Read")]
        public IActionResult Get()
        {
            IHeaderDictionary headers = _httpContextAccessor.HttpContext.Request.Headers;
            string accessToken = headers.Where(x => x.Key == "Authorization").Select(x => x.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(accessToken))
                return BadRequest("Invalid access token");

            accessToken = accessToken.Substring(7);

            UserAuthorization userAuthorization = new UserAuthorization();
            userAuthorization.ClientId = User.Claims.Where(x => x.Type == "client_id").Select(x => x.Value).FirstOrDefault();
            userAuthorization.IdentityProvider = User.Claims.Where(x => x.Type == "http://schemas.microsoft.com/identity/claims/identityprovider").Select(x => x.Value).FirstOrDefault();
            userAuthorization.DateTimeRequsted = DateTime.Now;
            userAuthorization.Registration.Email = User.Claims.Where(x => x.Type == "email").Select(x => x.Value).FirstOrDefault();

            Data.Registration registration = Data.Registration.Get(userAuthorization.Email, _dbContext);
            userAuthorization.Registration.RegistrationId = registration.Id;
            userAuthorization.Registration.FirstName = registration.FirstName;
            userAuthorization.Registration.LastName = registration.LastName;
            userAuthorization.Registration.RequestDate = registration.RequestDate;
            userAuthorization.Registration.ApprovedDate = registration.ApprovedDate;
            userAuthorization.Registration.ApprovedBy = (registration.ApprovedBy != null) ? registration.ApprovedBy.Email : string.Empty;
            userAuthorization.Registration.RevokedDate = registration.RevokedDate;
            userAuthorization.Registration.RevokedBy = (registration.RevokedBy != null) ? registration.RevokedBy.Email : string.Empty;

            string jsonString = JsonConvert.SerializeObject(userAuthorization);
            string encryptString = AESGCM.Encrypt(jsonString, accessToken);
            return new JsonResult(encryptString);
        }
    }
}
