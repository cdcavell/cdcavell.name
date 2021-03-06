using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CDCavell.ClassLibrary.Web.Services.Authorization
{
    /// <summary>
    /// RolePermission Web Service Model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.3 | 03/06/2021 | User Authorization Service |~ 
    /// </revision>
    public class RolePermissionModel
    {
        /// <value>long</value>
        [Required]
        [Display(Name = "RolePermission Id")]
        public long RolePermissionId { get; set; }

        /// <value>long</value>
        [Required]
        public long RegistrationId { get; set; }
        /// <value>Registration</value>
        public RegistrationModel Registration { get; set; }

        /// <value>long</value>
        [Required]
        public long RoleId { get; set; }
        /// <value>Role</value>
        public RoleModel Role { get; set; }


        /// <value>long</value>
        [Required]
        public long PermissionId { get; set; }
        /// <value>Permission</value>
        public PermissionModel Permission { get; set; }


        /// <value>long</value>
        [Required]
        public long StatusId { get; set; }
        /// <value>Permission</value>
        public StatusModel Status { get; set; }

        /// <value>List&lt;History&gt;</value>
        public List<HistoryModel> History { get; set; }
    }
}
