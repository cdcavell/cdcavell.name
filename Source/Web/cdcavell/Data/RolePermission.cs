using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cdcavell.Data
{
    /// <summary>
    /// CDCavell RolePermission Entity
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.9 | 11/03/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// </revision>
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
