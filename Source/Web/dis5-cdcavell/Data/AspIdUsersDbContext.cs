using dis5_cdcavell.Models.Account;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dis5_cdcavell.Data
{
    /// <summary>
    /// AspIdUsers Database Context
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.2.0 | 01/16/2021 | Initial build |~ 
    /// </revision>
    public class AspIdUsersDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="options">DbContextOptions&lt;AspIdUsersDbContext&gt;</param>
        /// <method>AspIdUsersDbContext(DbContextOptions&lt;AspIdUsersDbContext&gt; options) : base(options)</method>
        public AspIdUsersDbContext(DbContextOptions<AspIdUsersDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// OnModelCreating method
        /// </summary>
        /// <param name="builder">ModelBuilder</param>
        /// <method>OnModelCreating(ModelBuilder builder)</method>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
