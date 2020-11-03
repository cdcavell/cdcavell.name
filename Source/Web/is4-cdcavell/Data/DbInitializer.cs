﻿using Microsoft.EntityFrameworkCore;

namespace is4_cdcavell.Data
{
    /// <summary>
    /// AspIdUsersDbContext Database Initializer
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.7 | 10/31/2020 | Integrate Bing’s Adaptive URL submission API with your website [#144](https://github.com/cdcavell/cdcavell.name/issues/144) |~ 
    /// | Christopher D. Cavell | 1.0.0.9 | 11/02/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// </revision>
    public static class DbInitializer
    {
        /// <summary>
        /// Initialize method
        /// </summary>
        /// <param name="context">AspIdUsersDbContext</param>
        /// <method>Initialize(AspIdUsersDbContext context)</method>
        public static void Initialize(AspIdUsersDbContext context)
        {
            context.Database.Migrate();
        }
    }
}
