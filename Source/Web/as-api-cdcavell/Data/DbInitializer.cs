using CDCavell.ClassLibrary.Web.Mvc.Models.AppSettings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace as_api_cdcavell.Data
{
    /// <summary>
    /// Authorization Service Database Initializer
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/21/2021 | Initial build Authorization Service |~ 
    /// | Christopher D. Cavell | 1.0.3.3 | 03/09/2021 | User Authorization Service |~ 
    /// </revision>
    public static class DbInitializer
    {
        /// <summary>
        /// Initialize method
        /// &lt;br /&gt;
        /// To Initialize: dotnet ef migrations add InitialCreate --context AuthorizationServiceDbContext
        /// &lt;br /&gt;
        /// To Update:     dotnet ef migrations add UpdateDatabase_&lt;&lt;YYYY-MM-DD&gt;&gt;  --context AuthorizationServiceDbContext
        /// </summary>
        /// <param name="context">AuthorizationServiceDbContext</param>
        /// <param name="siteAdministrator">SiteAdministrator</param>
        /// <method>Initialize(MigrateDdContext context)</method>
        public static void Initialize(AuthorizationServiceDbContext context, SiteAdministrator siteAdministrator)
        {
            IEnumerable<string> pending = context.Database.GetPendingMigrations();
            if (pending.Any())
                context.Database.Migrate();

            // Look for any Status.
            if (!context.Status.Any())
            {
                var statuses = new Status[]
                {
                    new Status { Description = "Request" },
                    new Status { Description = "Approved" },
                    new Status { Description = "Denied" },
                    new Status { Description = "Revoked" },
                    new Status { Description = "Retired" }
                };

                foreach (Status status in statuses)
                    status.AddUpdate(context);
            }

            // Look for any Resource
            if (!context.Resource.Any())
            {
                var resource = new Resource
                {
                    ClientId = "Authorization.Service.UI",
                    Description = "Authorization Service"
                };

                resource.AddUpdate(context);

                // Look for any Roles
                if (!context.Role.Any())
                {
                    var role = new Role
                    {
                        Description = "Administration",
                        Resource = resource
                    };

                    role.AddUpdate(context);

                    // Look for any permissions
                    if (!context.Permission.Any())
                    {
                        var permission = new Permission
                        {
                            Description = "Registration",
                            Role = role
                        };

                        permission.AddUpdate(context);

                        // Look for any Registration
                        if (!context.Registration.Any())
                        {
                            var registration = new Registration
                            {
                                Email = siteAdministrator.Email,
                                FirstName = siteAdministrator.FirstName,
                                LastName = siteAdministrator.LastName,
                                RequestDate = DateTime.Now
                            };

                            registration.AddUpdate(context);

                            registration.ApprovedBy = registration;
                            registration.ApprovedDate = DateTime.Now;
                            registration.AddUpdate(context);

                            // Look for any RolePermission
                            if (!context.RolePermission.Any())
                            {
                                var approvedStatus = Status.GetByDescription("Approved", context);
                                var rolePermission = new RolePermission
                                {
                                    Role = role,
                                    Permission = permission,
                                    Registration = registration,
                                    Status = approvedStatus,
                                };

                                rolePermission.AddUpdate(context);

                                var history = new History
                                {
                                     ActionBy = registration,
                                     ActionOn = DateTime.Now,
                                     Description = "Site Initialization",
                                     RolePermission = rolePermission,
                                     Status = approvedStatus
                                };

                                history.AddUpdate(context);
                            }
                        }
                    }
                }
            }
        }
    }
}
