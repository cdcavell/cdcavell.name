using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public List<History> History { get; set; }

        #region Instance Methods

        #endregion

        #region Static Methods

        #endregion
    }
}
