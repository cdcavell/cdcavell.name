using Microsoft.EntityFrameworkCore;

namespace cdcavell.Data
{
    /// <summary>
    /// Migration Database Context
    /// &lt;br /&gt;
    /// To Initialize: dotnet ef migrations add InitialCreate --context MigrateDbContext
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.9 | 11/03/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// </revision>
    public class MigrateDbContext : DbContext
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="options">DbContextOptions</param>
        /// <method>MigrateDdContext(DbContextOptions&lt;TContext&gt; options) : base(options)</method>
        public MigrateDbContext(DbContextOptions<MigrateDbContext> options)
            : base(options)
        {
        }

        /// <value>DbSet&lt;AuditHistory&gt;</value>
        public DbSet<AuditHistory> AuditHistory { get; set; }
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
