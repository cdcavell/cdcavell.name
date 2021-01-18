using Microsoft.EntityFrameworkCore;
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
    /// | Christopher D. Cavell | 1.0.3.0 | 01/18/2021 | Initial build Authorization Service |~ 
    /// </revision>
    public static class DbInitializer
    {
        /// <summary>
        /// Initialize method
        /// &lt;br /&gt;
        /// To Initialize: dotnet ef migrations add InitialCreate
        /// &lt;br /&gt;
        /// To Update:     dotnet ef migrations add UpdateDatabase_&lt;&lt;YYYY-MM-DD&gt;&gt;
        /// </summary>
        /// <param name="context">CDCavellDdContext</param>
        /// <method>Initialize(MigrateDdContext context)</method>
        public static void Initialize(AuthorizationServiceDbContext context)
        {
            IEnumerable<string> pending = context.Database.GetPendingMigrations();
            if (pending.Any())
                context.Database.Migrate(); 
        }
    }
}
