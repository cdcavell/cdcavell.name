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
    /// <class>AuthorizationController</class>
    /// <summary>
    /// User authorization controller class
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 02/01/2021 | Initial build Authorization Service |~ 
    /// | Christopher D. Cavell | 1.0.3.1 | 02/08/2021 | User Authorization Web Service |~ 
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

            userAuthorization.Registration.RegistrationId = registration.Id;
            userAuthorization.Registration.Email = registration.Email;
            userAuthorization.Registration.FirstName = registration.FirstName;
            userAuthorization.Registration.LastName = registration.LastName;
            userAuthorization.Registration.RequestDate = registration.RequestDate;
            userAuthorization.Registration.ApprovedDate = registration.ApprovedDate;
            userAuthorization.Registration.ApprovedBy = (registration.ApprovedBy != null) ? registration.ApprovedBy.Email : string.Empty;
            userAuthorization.Registration.RevokedDate = registration.RevokedDate;
            userAuthorization.Registration.RevokedBy = (registration.RevokedBy != null) ? registration.RevokedBy.Email : string.Empty;

            jsonString = JsonConvert.SerializeObject(userAuthorization);
            string encryptString = AESGCM.Encrypt(jsonString, accessToken);
            return new JsonResult(encryptString);
        }

        /// <summary>
        /// Put action method
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

            Data.Registration registration = Data.Registration.Get(
                userAuthorization.Email.Clean(),
                _dbContext
            );

            registration.Delete(_dbContext);

            return Ok();
        }
    }
}
