using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace cdcavell.Data
{
    /// <summary>
    /// CDCavell Database Initializer
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.7 | 10/31/2020 | Integrate Bing’s Adaptive URL submission API with your website [#144](https://github.com/cdcavell/cdcavell.name/issues/144) |~ 
    /// | Christopher D. Cavell | 1.0.0.9 | 11/11/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// | Christopher D. Cavell | 1.0.3.0 | 01/23/2021 | Initial build Authorization Service |~ 
    /// </revision>
    public static class DbInitializer
    {
        /// <summary>
        /// Initialize method
        /// &lt;br /&gt;
        /// To Initialize: dotnet ef migrations add InitialCreate --context CDCavellDbContext
        /// &lt;br /&gt;
        /// To Update:     dotnet ef migrations add UpdateDatabase_&lt;&lt;YYYY-MM-DD&gt;&gt; --context CDCavellDbContext
        /// &lt;br /&gt;
        /// To Revert:     dotnet ef database update &lt;previous migration name&gt; (Then - dotnet ef migrations remove)  
        /// </summary>
        /// <param name="context">CDCavellDdContext</param>
        /// <method>Initialize(MigrateDdContext context)</method>
        public static void Initialize(CDCavellDbContext context)
        {
            IEnumerable<string> pending = context.Database.GetPendingMigrations();
            if (pending.Any())
                context.Database.Migrate(); 
        }
    }
}
