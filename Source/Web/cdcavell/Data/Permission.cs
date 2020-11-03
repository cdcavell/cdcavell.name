using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cdcavell.Data
{
    /// <summary>
    /// CDCavell Permission Entity
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.9 | 11/02/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// </revision>
    public class Permission
    {
        /// <value>int</value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PermissionId { get; set; }
        /// <value>string</value>
        public string Description { get; set; }


        /// <value>int</value>
        public int RoleId { get; set; }
        /// <value>Role</value>
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
}
