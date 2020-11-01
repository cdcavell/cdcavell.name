using Microsoft.EntityFrameworkCore;

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

        /// <value>SiteMap</value>
        public DbSet<SiteMap> SiteMap { get; set; }

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
