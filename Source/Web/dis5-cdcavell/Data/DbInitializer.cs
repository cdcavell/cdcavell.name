using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace dis5_cdcavell.Data
{
    /// <summary>
    /// AspIdUsersDbContext Database Initializer
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.2.0 | 01/16/2021 | Initial build |~ 
    /// </revision>
    public static class DbInitializer
    {
        /// <summary>
        /// Initialize method
        /// &lt;br /&gt;
        /// To Initialize: dotnet ef migrations add InitialCreate
        /// &lt;br /&gt;
        /// To Update:     dotnet ef migrations add UpdateDatabase
        /// </summary>
        /// <param name="context">AspIdUsersDbContext</param>
        /// <method>Initialize(AspIdUsersDbContext context)</method>
        public static void Initialize(AspIdUsersDbContext context)
        {
            IEnumerable<string> pending = context.Database.GetPendingMigrations();
            if (pending.Count() > 0)
                context.Database.Migrate();
        }
    }
}
