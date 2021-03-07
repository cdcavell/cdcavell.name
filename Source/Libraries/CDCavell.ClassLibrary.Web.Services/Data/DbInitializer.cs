using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CDCavell.ClassLibrary.Web.Services.Data
{
    /// <summary>
    /// Authorization UI Database Initializer
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.3 | 03/07/2021 | User Authorization Service |~ 
    /// </revision>
    public static class DbInitializer
    {
        /// <summary>
        /// Initialize method
        /// &lt;br /&gt;
        /// To Initialize: dotnet ef migrations add InitialCreate
        /// &lt;br /&gt;
        /// To Update:     dotnet ef migrations add UpdateDatabase_&lt;&lt;YYYY-MM-DD&gt;&gt;
        /// &lt;br /&gt;
        /// To Revert:     dotnet ef database update &lt;previous migration name&gt; (Then - dotnet ef migrations remove)  
        /// </summary>
        /// <param name="context">AuthorizationDbContext</param>
        /// <method>Initialize(MigrateDdContext context)</method>
        public static void Initialize(AuthorizationDbContext context)
        {
            IEnumerable<string> pending = context.Database.GetPendingMigrations();
            if (pending.Any())
                context.Database.Migrate(); 
        }
    }
}
