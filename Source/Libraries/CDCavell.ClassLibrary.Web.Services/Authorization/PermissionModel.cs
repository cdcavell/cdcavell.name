using System.ComponentModel.DataAnnotations;

namespace CDCavell.ClassLibrary.Web.Services.Authorization
{
    /// <summary>
    /// Permission Web Service Model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.3 | 02/27/2021 | User Authorization Service |~ 
    /// </revision>
    public class PermissionModel
    {
        /// <value>long</value>
        [Required]
        [Display(Name = "Permission Id")]
        public long PermissionId { get; set; }

        /// <value>string</value>
        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        /// <value>long</value>
        [Required]
        public long RoleId { get; set; }
        /// <value>Role</value>
        public RoleModel Role { get; set; }
    }
}
