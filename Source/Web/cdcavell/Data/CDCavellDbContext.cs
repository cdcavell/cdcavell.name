using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace cdcavell.Data
{
    /// <summary>
    /// CDCavell Database Context
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.7 | 10/31/2020 | Integrate Bing’s Adaptive URL submission API with your website [#144](https://github.com/cdcavell/cdcavell.name/issues/144) |~ 
    /// | Christopher D. Cavell | 1.0.0.9 | 11/02/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// </revision>
    public class CDCavellDbContext : DbContext
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="options">DbContextOptions</param>
        /// <method>CDCavellDdContext(DbContextOptions options) : base(options)</method>
        public CDCavellDbContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <value>DbSet&lt;SiteMap&gt;</value>
        public DbSet<SiteMap> SiteMap { get; set; }
        /// <value>DbSet&lt;Registration&gt;</value>
        public DbSet<Registration> Registration { get; set; }
        /// <value>DbSet&lt;Role&gt;</value>
        public DbSet<Role> Role { get; set; }
        /// <value>DbSet&lt;Permission&gt;</value>
        public DbSet<Permission> Permission { get; set; }
        /// <value>DbSet&lt;RolePermission&gt;</value>
        public DbSet<RolePermission> RolePermission { get; set; }

        /// <summary>
        /// OnModelCreating method
        /// </summary>
        /// <param name="builder">ModelBuilder</param>
        /// <method>OnModelCreating(ModelBuilder builder)</method>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
