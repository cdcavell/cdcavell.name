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
    /// | Christopher D. Cavell | 1.0.3.0 | 01/19/2021 | Initial build Authorization Service |~ 
    /// </revision>
    [Table("RolePermission")]
    public class RolePermission : DataModel<RolePermission>
    {
        /// <value>int</value>
        [Required]
        public int RegistrationId { get; set; }
        /// <value>Registration</value>
        [ForeignKey("RegistrationId")]
        public Registration Registration { get; set; }


        /// <value>int</value>
        [Required]
        public int RoleId { get; set; }
        /// <value>Role</value>
        [ForeignKey("RoleId")]
        public Role Role { get; set; }


        /// <value>int</value>
        [Required]
        public int PermissionId { get; set; }
        /// <value>Permission</value>
        [ForeignKey("PermissionId")]
        public Permission Permission { get; set; }

        #region Instance Methods

        #endregion

        #region Static Methods

        #endregion
    }
}
