﻿using Microsoft.EntityFrameworkCore;
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
    /// </revision>
    public static class DbInitializer
    {
        /// <summary>
        /// Initialize method
        /// &lt;br /&gt;
        /// To Initialize: dotnet ef migrations add InitialCreate --project as-api-cdcavell.csproj --context AuthorizationServiceDbContext
        /// &lt;br /&gt;
        /// To Update:     dotnet ef migrations add UpdateDatabase_&lt;&lt;YYYY-MM-DD&gt;&gt;  --project as-api-cdcavell.csproj --context AuthorizationServiceDbContext
        /// </summary>
        /// <param name="context">CDCavellDdContext</param>
        /// <method>Initialize(MigrateDdContext context)</method>
        public static void Initialize(AuthorizationServiceDbContext context)
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
                {
                    context.Status.Add(status);
                }

                context.SaveChanges();
            }
        }
    }
}
