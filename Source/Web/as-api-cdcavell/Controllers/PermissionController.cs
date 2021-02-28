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
                var rolePermissionModel = new RolePermissionModel()
                {
                    RolePermissionId = rolePermission.Id,
                    RegistrationId = rolePermission.RegistrationId,
                    Registration = new RegistrationModel()
                    {
                        RegistrationId = rolePermission.Registration.Id,
                        Email = rolePermission.Registration.Email ?? string.Empty,
                        FirstName = rolePermission.Registration.FirstName,
                        LastName = rolePermission.Registration.LastName,
                        RequestDate = rolePermission.Registration.RequestDate,
                        ApprovedDate = rolePermission.Registration.ApprovedDate,
                        ApprovedBy = (rolePermission.Registration.ApprovedBy != null) ? rolePermission.Registration.ApprovedBy.Email ?? string.Empty : string.Empty,
                        RevokedDate = rolePermission.Registration.RevokedDate,
                        RevokedBy = (rolePermission.Registration.RevokedBy != null) ? rolePermission.Registration.RevokedBy.Email ?? string.Empty : string.Empty
                    },
                    RoleId = rolePermission.RoleId,
                    Role = new RoleModel()
                    {
                        RoleId = rolePermission.Role.Id,
                        Description = rolePermission.Role.Description,
                        ResourceId = rolePermission.Role.ResourceId,
                        Resource = new ResourceModel()
                        {
                            ResourceId = rolePermission.Role.Resource.Id,
                            ClientId = rolePermission.Role.Resource.ClientId,
                            Description = rolePermission.Role.Resource.Description
                        }
                    },
                    PermissionId = rolePermission.PermissionId,
                    Permission = new PermissionModel()
                    {
                        PermissionId = rolePermission.Permission.Id,
                        Description = rolePermission.Permission.Description,
                        RoleId = rolePermission.Permission.RoleId,
                        Role = new RoleModel()
                        {
                            RoleId = rolePermission.Permission.Role.Id,
                            Description = rolePermission.Permission.Role.Description,
                            ResourceId = rolePermission.Permission.Role.ResourceId,
                            Resource = new ResourceModel()
                            {
                                ResourceId = rolePermission.Permission.Role.Resource.Id,
                                ClientId = rolePermission.Permission.Role.Resource.ClientId,
                                Description = rolePermission.Permission.Role.Resource.Description
                            }
                        }
                    },
                    StatusId = rolePermission.StatusId,
                    Status = new StatusModel()
                    {
                        StatusId = rolePermission.Status.Id,
                        Description = rolePermission.Status.Description
                    }
                };

                rolePermissionModel.History = new List<HistoryModel>();
                foreach (History history in rolePermission.History)
                {
                    HistoryModel historyModel = new HistoryModel()
                    {
                        HistoryId = history.Id,
                        ActionOn = history.ActionOn,
                        ActionById = history.ActionById,
                        ActionBy = new RegistrationModel()
                        {
                            RegistrationId = history.ActionBy.Id,
                            Email = history.ActionBy.Email ?? string.Empty,
                            FirstName = history.ActionBy.FirstName,
                            LastName = history.ActionBy.LastName,
                            RequestDate = history.ActionBy.RequestDate,
                            ApprovedDate = history.ActionBy.ApprovedDate,
                            ApprovedBy = (history.ActionBy.ApprovedBy != null) ? history.ActionBy.ApprovedBy.Email ?? string.Empty : string.Empty,
                            RevokedDate = history.ActionBy.RevokedDate,
                            RevokedBy = (history.ActionBy.RevokedBy != null) ? history.ActionBy.RevokedBy.Email ?? string.Empty : string.Empty
                        },
                        RolePermissionId = history.RolePermissionId,
                        RolePermission = new RolePermissionModel()
                        {
                            RolePermissionId = history.RolePermission.Id,
                            RegistrationId = history.RolePermission.RegistrationId,
                            Registration = new RegistrationModel()
                            {
                                RegistrationId = history.RolePermission.Registration.Id,
                                Email = history.RolePermission.Registration.Email ?? string.Empty,
                                FirstName = history.RolePermission.Registration.FirstName,
                                LastName = history.RolePermission.Registration.LastName,
                                RequestDate = history.RolePermission.Registration.RequestDate,
                                ApprovedDate = history.RolePermission.Registration.ApprovedDate,
                                ApprovedBy = (history.RolePermission.Registration.ApprovedBy != null) ? history.RolePermission.Registration.ApprovedBy.Email ?? string.Empty : string.Empty,
                                RevokedDate = history.RolePermission.Registration.RevokedDate,
                                RevokedBy = (history.RolePermission.Registration.RevokedBy != null) ? history.RolePermission.Registration.RevokedBy.Email ?? string.Empty : string.Empty
                            },
                            RoleId = history.RolePermission.RoleId,
                            Role = new RoleModel()
                            {
                                RoleId = history.RolePermission.Role.Id,
                                Description = history.RolePermission.Role.Description,
                                ResourceId = history.RolePermission.Role.ResourceId,
                                Resource = new ResourceModel()
                                {
                                    ResourceId = history.RolePermission.Role.Resource.Id,
                                    ClientId = history.RolePermission.Role.Resource.ClientId,
                                    Description = history.RolePermission.Role.Resource.Description
                                }
                            },
                            PermissionId = history.RolePermission.PermissionId,
                            Permission = new PermissionModel()
                            {
                                PermissionId = history.RolePermission.Permission.Id,
                                Description = history.RolePermission.Permission.Description,
                                RoleId = history.RolePermission.Permission.RoleId,
                                Role = new RoleModel()
                                {
                                    RoleId = history.RolePermission.Permission.Role.Id,
                                    Description = history.RolePermission.Permission.Role.Description,
                                    ResourceId = history.RolePermission.Permission.Role.ResourceId,
                                    Resource = new ResourceModel()
                                    {
                                        ResourceId = history.RolePermission.Permission.Role.Resource.Id,
                                        ClientId = history.RolePermission.Permission.Role.Resource.ClientId,
                                        Description = history.RolePermission.Permission.Role.Resource.Description
                                    }
                                }
                            },
                            StatusId = history.RolePermission.StatusId,
                            Status = new StatusModel()
                            {
                                StatusId = history.RolePermission.Status.Id,
                                Description = history.RolePermission.Status.Description
                            }
                        },
                        StatusId = history.StatusId,
                        Status = new StatusModel()
                        {
                            StatusId = history.Status.Id,
                            Description = history.Status.Description
                        },
                        Description = history.Description
                    };

                    rolePermissionModel.History.Add(historyModel);
                }

                userAuthorization.RolePermissions.Add(rolePermissionModel);
            }

            jsonString = JsonConvert.SerializeObject(userAuthorization);
            string encryptString = AESGCM.Encrypt(jsonString, accessToken);
            return new JsonResult(encryptString);
        }
    }
}