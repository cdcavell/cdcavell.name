using CDCavell.ClassLibrary.Commons.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace CDCavell.ClassLibrary.Web.Services.Data
{
    /// <summary>
    /// Authorization UI Database Context
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.3 | 03/07/2021 | User Authorization Service |~ 
    /// </revision>
    public class AuthorizationDbContext : DbContext
    {
        private readonly Logger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _emailAddress;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="logger">ILogger&lt;AuthorizationDbContext&gt;</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        /// <param name="options">DbContextOptions</param>
        /// <method>CDCavellDdContext(ILogger&lt;CDCavellDbContext&gt; logger, IHttpContextAccessor httpContextAccessor, DbContextOptions&lt;CDCavellDbContext&gt; options) : base(options)</method>
        public AuthorizationDbContext(ILogger<AuthorizationDbContext> logger, IHttpContextAccessor httpContextAccessor, DbContextOptions<AuthorizationDbContext> options)
            : base(options)
        {
            _logger = new Logger(logger);
            _httpContextAccessor = httpContextAccessor;

            if (_httpContextAccessor.HttpContext != null)
            {
                var user = _httpContextAccessor.HttpContext.User;
                Claim emailAddressClaim = (user != null) ? user.FindFirst(ClaimTypes.Email) ?? user.FindFirst("email") : null;
                if (emailAddressClaim != null)
                    _emailAddress = emailAddressClaim.Value ?? "Anonymous";
                else
                    _emailAddress = "Anonymous";
            }
            else
            {
                string file = this.GetType().Assembly.Location;
                string app = System.IO.Path.GetFileNameWithoutExtension(file);
                _emailAddress = app;
            }
        }

        /// <value>DbSet&lt;AuditHistory&gt;</value>
        public DbSet<AuditHistory> AuditHistory { get; set; }
        /// <value>DbSet&lt;Authorization&gt;</value>
        public DbSet<Authorization> Authorization { get; set; }

        /// <summary>
        /// OnModelCreating method
        /// </summary>
        /// <param name="builder">ModelBuilder</param>
        /// <method>OnModelCreating(ModelBuilder builder)</method>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Authorization>()
                .HasIndex(x => new { x.ClientId, x.Guid })
                .IsUnique();

            base.OnModelCreating(builder);
        }

        /// <summary>
        /// Checks for any unsaved INSERT, UPDATE, DELETE history.
        /// </summary>
        /// <returns>bool</returns>
        /// <method>HasUnsavedChanges()</method>
        public bool HasUnsavedChanges()
        {
            return ChangeTracker.HasChanges();
        }

        /// <summary>
        /// Override to record all the data change history in a table named ```Audit```, this table 
        /// contains INSERT, UPDATE, DELETE history.
        /// </summary>
        /// <returns>int</returns>
        /// <method>SaveChanges(bool acceptAllChangesOnSuccess = true)</method>
        public override int SaveChanges(bool acceptAllChangesOnSuccess = true)
        {
            // uncomment to have audit records greated than 7 days old removed from dbo.AuditHistory table
            RemoveAuditRecords();

            List<AuditEntry> auditEntries = OnBeforeSaveChanges(_emailAddress);
            int result = base.SaveChanges(acceptAllChangesOnSuccess);
            OnAfterSaveChanges(auditEntries);

            return result;
        }

        /// <summary>
        /// Override to record all the data change history in a table named ```Audit```, this table 
        /// contains INSERT, UPDATE, DELETE history.
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Task&lt;int&gt;</returns>
        /// <method>SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))</method>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // uncomment to have audit records greated than 7 days old removed from dbo.AuditHistory table
            RemoveAuditRecords();

            List<AuditEntry> auditEntries = OnBeforeSaveChanges(_emailAddress);
            int result = await base.SaveChangesAsync(true, cancellationToken);
            await OnAfterSaveChangesAsync(auditEntries);

            return result;
        }

        /// <summary>
        /// Save audit entities that have all the modifications and return list of entries 
        /// where the value of some properties are unknown at this step.
        /// &lt;br/&gt;&lt;br/&gt;
        /// https://www.meziantou.net/entity-framework-core-history-audit-table.htm
        /// </summary>
        /// <returns>List&lt;AuditEntry&gt;</returns>
        /// <method>OnBeforeSaveChanges(string userId)</method>
        private List<AuditEntry> OnBeforeSaveChanges(string userId)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditHistory || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Metadata.GetTableName();
                auditEntry.ModifiedBy = userId;
                auditEntry.ModifiedOn = DateTime.UtcNow;
                auditEntry.State = entry.State.ToString();
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        // value will be generated by the database, get the value after saving
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.CurrentValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.OriginalValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.OriginalValues[propertyName] = property.OriginalValue;
                                auditEntry.CurrentValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }

            // Save audit entities that have all the modifications
            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                AuditHistory.Add(auditEntry.ToAuditHistory());
            }

            // keep a list of entries where the value of some properties are unknown at this step
            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        /// <summary>
        /// Save audit entities where the value of some properties were unknown at previous step.
        /// &lt;br/&gt;&lt;br/&gt;
        /// https://www.meziantou.net/entity-framework-core-history-audit-table.htm
        /// </summary>
        /// <method>OnAfterSaveChanges(List&lt;AuditEntry&gt; auditEntries)</method>
        private void OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return;

            foreach (var auditEntry in auditEntries)
            {
                // Get the final value of the temporary properties
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.CurrentValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }

                // Save the Audit entry
                AuditHistory.Add(auditEntry.ToAuditHistory());
                SaveChanges();
            }

            return;
        }

        /// <summary>
        /// Save audit entities where the value of some properties were unknown at previous step.
        /// &lt;br/&gt;&lt;br/&gt;
        /// https://www.meziantou.net/entity-framework-core-history-audit-table.htm
        /// </summary>
        /// <method>OnAfterSaveChangesAsync(List&lt;AuditEntry&gt; auditEntries)</method>
        private Task OnAfterSaveChangesAsync(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            foreach (var auditEntry in auditEntries)
            {
                // Get the final value of the temporary properties
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.CurrentValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }

                // Save the Audit entry
                AuditHistory.Add(auditEntry.ToAuditHistory());
            }

            return SaveChangesAsync();
        }

        /// <summary>
        /// Remove audit records greater than 7 days old
        /// </summary>
        /// <method>RemoveAuditRecords()</method>
        private void RemoveAuditRecords()
        {
            var query = this.AuditHistory.Where(Audit => Audit.ModifiedOn < DateTime.Now.AddDays(-7));
            if (query.Count() > 0)
                this.AuditHistory.RemoveRange(query);
        }
    }
}
