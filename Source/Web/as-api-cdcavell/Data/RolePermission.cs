using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace as_api_cdcavell.Data
{
    /// <summary>
    /// RolePermission Entity
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/20/2021 | Initial build Authorization Service |~ 
    /// | Christopher D. Cavell | 1.0.3.3 | 02/27/2021 | User Authorization Service |~ 
    /// </revision>
    [Table("RolePermission")]
    public class RolePermission : DataModel<RolePermission>
    {
        /// <value>long</value>
        [Required]
        public long RegistrationId { get; set; }
        /// <value>Registration</value>
        [ForeignKey("RegistrationId")]
        public Registration Registration { get; set; }

        /// <value>long</value>
        [Required]
        public long RoleId { get; set; }
        /// <value>Role</value>
        [ForeignKey("RoleId")]
        public Role Role { get; set; }


        /// <value>long</value>
        [Required]
        public long PermissionId { get; set; }
        /// <value>Permission</value>
        [ForeignKey("PermissionId")]
        public Permission Permission { get; set; }


        /// <value>long</value>
        [Required]
        public long StatusId { get; set; }
        /// <value>Permission</value>
        [ForeignKey("StatusId")]
        public Status Status { get; set; }

        /// <value>List&lt;History&gt;</value>
        public List<History> History { get; set; } = new List<History>();

        #region Instance Methods

        #endregion

        #region Static Methods

        /// <summary>
        /// Get By RegistrationId
        /// </summary>
        /// <param name="registrationId">long</param>
        /// <param name="dbContext">AuthorizationServiceDbContext</param>
        /// <returns></returns>
        public static List<RolePermission> GetByRegistrationId(long registrationId, AuthorizationServiceDbContext dbContext)
        {
            List<RolePermission> rolePermissions = dbContext.RolePermission
                .Include("Registration")
                .Include("Role.Resource")
                .Include("Permission")
                .Include("Status")
                .Include("History")
                .Where(x => x.RegistrationId == registrationId)
                .ToList();

            return rolePermissions;
        }

        #endregion
    }
}
