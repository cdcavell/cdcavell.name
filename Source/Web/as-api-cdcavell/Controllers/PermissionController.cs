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
using System.Collections.Generic;
using System.Linq;

namespace as_api_cdcavell.Controllers
{
    /// <class>PermissionController</class>
    /// <summary>
    /// User permissions controller class
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.3 | 02/27/2021 | User Authorization Web Service |~ 
    /// </revision>
    public class PermissionController : ApplicationBaseController<PermissionController>
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="logger">ILogger&lt;PermissionController&gt;</param>
        /// <param name="webHostEnvironment">IWebHostEnvironment</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        /// <param name="authorizationService">IAuthorizationService</param>
        /// <param name="appSettings">AppSettings</param>
        /// <param name="dbContext">AuthorizationServiceDbContext</param>
        /// <method>
        /// public PermissionController(
        ///     ILogger&lt;PermissionController&gt; logger,
        ///     IWebHostEnvironment webHostEnvironment,
        ///     IHttpContextAccessor httpContextAccessor,
        ///     IAuthorizationService authorizationService,
        ///     AppSettings appSettings,
        ///     AuthorizationServiceDbContext dbContext
        /// ) : base(logger, webHostEnvironment, httpContextAccessor, authorizationService, appSettings, dbContext)
        /// </method>
        public PermissionController(
            ILogger<PermissionController> logger,
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
        public IActionResult Get(object encryptObject)
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
                return BadRequest("Invalid Registration");

            List<Data.RolePermission> rolePermissions = Data.RolePermission.GetByRegistrationId(
                registration.Id,
                _dbContext
            );

            userAuthorization.RolePermissions.Clear();
            foreach (Data.RolePermission rolePermission in rolePermissions)
            {
                userAuthorization.RolePermissions.Add(
                    new RolePermissionModel()
                    {
                        RolePermissionId = rolePermission.Id,
                        RegistrationId = rolePermission.RegistrationId,
                        //Registration = new RegistrationModel()
                        //{
                        //    RegistrationId = rolePermission.Registration.Id,
                        //    Email = rolePermission.Registration.Email ?? string.Empty,
                        //    FirstName = rolePermission.Registration.FirstName,
                        //    LastName = rolePermission.Registration.LastName,
                        //    RequestDate = rolePermission.Registration.RequestDate,
                        //    ApprovedDate = rolePermission.Registration.ApprovedDate,
                        //    ApprovedBy = (rolePermission.Registration.ApprovedBy != null) ? rolePermission.Registration.ApprovedBy.Email ?? string.Empty : string.Empty,
                        //    RevokedDate = rolePermission.Registration.RevokedDate,
                        //    RevokedBy = (rolePermission.Registration.RevokedBy != null) ? rolePermission.Registration.RevokedBy.Email ?? string.Empty : string.Empty
                        //},
                        RoleId = rolePermission.RoleId,
                        //Role = new RoleModel()
                        //{
                        //    RoleId = rolePermission.Role.Id,
                        //    Description = rolePermission.Role.Description,
                        //    ResourceId = rolePermission.Role.ResourceId,
                        //    Resource = new ResourceModel()
                        //    {
                        //        ResourceId = rolePermission.Role.Resource.Id,
                        //        ClientId = rolePermission.Role.Resource.ClientId,
                        //        Description = rolePermission.Role.Resource.Description
                        //    }
                        //},
                        PermissionId = rolePermission.PermissionId,
                        //Permission = new PermissionModel()
                        //{
                        //    PermissionId = rolePermission.Permission.Id,
                        //    Description = rolePermission.Permission.Description,
                        //    RoleId = rolePermission.Permission.RoleId,
                        //    Role = new RoleModel()
                        //    {
                        //        RoleId = rolePermission.Permission.Role.Id,
                        //        Description = rolePermission.Permission.Role.Description,
                        //        ResourceId = rolePermission.Permission.Role.ResourceId,
                        //        Resource = new ResourceModel()
                        //        {
                        //            ResourceId = rolePermission.Permission.Role.Resource.Id,
                        //            ClientId = rolePermission.Permission.Role.Resource.ClientId,
                        //            Description = rolePermission.Permission.Role.Resource.Description
                        //        }
                        //    }
                        //},
                        StatusId = rolePermission.StatusId,
                    }
                );
            }

            jsonString = JsonConvert.SerializeObject(userAuthorization);
            string encryptString = AESGCM.Encrypt(jsonString, accessToken);
            return new JsonResult(encryptString);
        }
    }
}