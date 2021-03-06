using as_api_cdcavell.Data;
using as_api_cdcavell.Models.AppSettings;
using CDCavell.ClassLibrary.Web.Security;
using CDCavell.ClassLibrary.Web.Services.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace as_api_cdcavell.Controllers
{
    /// <class>RegistrationController</class>
    /// <summary>
    /// User registration controller class
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 02/01/2021 | Initial build Authorization Service |~ 
    /// | Christopher D. Cavell | 1.0.3.1 | 02/08/2021 | User Authorization Web Service |~ 
    /// | Christopher D. Cavell | 1.0.3.3 | 03/06/2021 | User Authorization Web Service |~ 
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
        [Authorize(Policy = "Read")]
        public IActionResult Get()
        {
            IHeaderDictionary headers = _httpContextAccessor.HttpContext.Request.Headers;
            string accessToken = headers.Where(x => x.Key == "Authorization").Select(x => x.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(accessToken))
                return BadRequest("Invalid access token");

            accessToken = accessToken.Substring(7);

            UserAuthorizationModel userAuthorization = new UserAuthorizationModel();

            string  email = User.Claims.Where(x => x.Type == "email").Select(x => x.Value).FirstOrDefault();
            if (!string.IsNullOrEmpty(email))
            {
                Data.Registration registration = Data.Registration.Get(
                    email.Clean(),
                    _dbContext
                );

                userAuthorization.Registration.RegistrationId = registration.Id;
                userAuthorization.Registration.Email = registration.Email ?? registration.Email;
                userAuthorization.Registration.FirstName = registration.FirstName;
                userAuthorization.Registration.LastName = registration.LastName;
                userAuthorization.Registration.RequestDate = registration.RequestDate;
                userAuthorization.Registration.ApprovedDate = registration.ApprovedDate;
                userAuthorization.Registration.ApprovedBy = (registration.ApprovedBy != null) ? registration.ApprovedBy.Email ?? string.Empty : string.Empty;
                userAuthorization.Registration.RevokedDate = registration.RevokedDate;
                userAuthorization.Registration.RevokedBy = (registration.RevokedBy != null) ? registration.RevokedBy.Email ?? string.Empty : string.Empty;

                userAuthorization.ClientId = User.Claims.Where(x => x.Type == "client_id").Select(x => x.Value).FirstOrDefault();
                userAuthorization.IdentityProvider = User.Claims.Where(x => x.Type == "http://schemas.microsoft.com/identity/claims/identityprovider").Select(x => x.Value).FirstOrDefault();
                userAuthorization.DateTimeRequsted = DateTime.Now;
            }

            string jsonString = JsonConvert.SerializeObject(userAuthorization);
            string encryptString = AESGCM.Encrypt(jsonString, accessToken);
            return new JsonResult(encryptString);
        }

        /// <summary>
        /// Put action method
        /// </summary>
        /// <param name="encryptObject">object</param>
        [HttpPut]
        [Authorize(Policy = "Write")]
        public IActionResult Put(object encryptObject)
        {
            IHeaderDictionary headers = _httpContextAccessor.HttpContext.Request.Headers;
            string accessToken = headers.Where(x => x.Key == "Authorization").Select(x => x.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(accessToken))
                return BadRequest("Invalid Authorization");

            accessToken = accessToken.Substring(7);

            string jsonString = AESGCM.Decrypt(encryptObject.ToString(), accessToken);
            UserAuthorizationModel userAuthorization = JsonConvert.DeserializeObject<UserAuthorizationModel>(jsonString);

            Data.Registration registration = Data.Registration.Get(
                userAuthorization.Email.Clean(),
                _dbContext
            );

            if (registration.IsNew)
            {
                registration.Email = userAuthorization.Registration.Email.Clean();
                registration.FirstName = userAuthorization.Registration.FirstName.Clean();
                registration.LastName = userAuthorization.Registration.LastName.Clean();
                registration.RequestDate = DateTime.Now;
                registration.AddUpdate(_dbContext);
            }

            if (!registration.IsNew)
                if (!registration.IsRevoked)
                    if (registration.IsRegistered)
                    {
                        registration.FirstName = userAuthorization.Registration.FirstName.Clean();
                        registration.LastName = userAuthorization.Registration.LastName.Clean();
                        registration.AddUpdate(_dbContext);
                    }

            // Allow self revoked to register again
            if (registration.IsRevoked)
                if (registration.Id.Equals(registration.RevokedById))
                {
                    registration.Email = userAuthorization.Registration.Email.Clean();
                    registration.FirstName = userAuthorization.Registration.FirstName.Clean();
                    registration.LastName = userAuthorization.Registration.LastName.Clean();
                    registration.RequestDate = DateTime.Now;
                    registration.ApprovedById = null;
                    registration.ApprovedBy = null;
                    registration.ApprovedDate = DateTime.MinValue;
                    registration.RevokedById = null;
                    registration.RevokedBy = null;
                    registration.RevokedDate = DateTime.MinValue;
                    registration.AddUpdate(_dbContext);
                }

            userAuthorization.Registration.RegistrationId = registration.Id;
            userAuthorization.Registration.Email = registration.Email ?? string.Empty;
            userAuthorization.Registration.FirstName = registration.FirstName;
            userAuthorization.Registration.LastName = registration.LastName;
            userAuthorization.Registration.RequestDate = registration.RequestDate;
            userAuthorization.Registration.ApprovedDate = registration.ApprovedDate;
            userAuthorization.Registration.ApprovedBy = (registration.ApprovedBy != null) ? registration.ApprovedBy.Email ?? string.Empty : string.Empty;
            userAuthorization.Registration.RevokedDate = registration.RevokedDate;
            userAuthorization.Registration.RevokedBy = (registration.RevokedBy != null) ? registration.RevokedBy.Email ?? string.Empty : string.Empty;

            jsonString = JsonConvert.SerializeObject(userAuthorization);
            string encryptString = AESGCM.Encrypt(jsonString, accessToken);
            return new JsonResult(encryptString);
        }

        /// <summary>
        /// Delete action method
        /// </summary>
        /// <param name="encryptObject">object</param>
        [HttpDelete]
        [Authorize(Policy = "Write")]
        public IActionResult Delete(object encryptObject)
        {
            IHeaderDictionary headers = _httpContextAccessor.HttpContext.Request.Headers;
            string accessToken = headers.Where(x => x.Key == "Authorization").Select(x => x.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(accessToken))
                return BadRequest("Invalid Authorization");

            accessToken = accessToken.Substring(7);

            string jsonString = AESGCM.Decrypt(encryptObject.ToString(), accessToken);
            UserAuthorizationModel userAuthorization = JsonConvert.DeserializeObject<UserAuthorizationModel>(jsonString);

            Data.Registration registration = Data.Registration
                .Get(userAuthorization.Email.Clean(), _dbContext);

            registration.ApprovedById = null;
            registration.ApprovedBy = null;
            registration.ApprovedDate = DateTime.MinValue;
            registration.RevokedById = registration.Id;
            registration.RevokedBy = registration;
            registration.RevokedDate = DateTime.Now;
            registration.AddUpdate(_dbContext);

            return Ok();
        }
    }
}
