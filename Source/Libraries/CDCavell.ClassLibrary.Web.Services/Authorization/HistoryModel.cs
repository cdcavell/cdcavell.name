using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDCavell.ClassLibrary.Web.Services.Authorization
{
    /// <summary>
    /// History Entity
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.3 | 02/27/2021 | User Authorization Service |~ 
    /// </revision>
    [Table("History")]
    public class HistoryModel
    {
        /// <value>long</value>
        [Required]
        [Display(Name = "History Id")]
        public long HistoryId { get; set; }

        /// <value>DateTime</value>
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ActionOn { get; set; }

        /// <value>long</value>
        [Required]
        public long ActionById { get; set; }
        /// <value>Registration</value>
        [ForeignKey("ActionById")]
        public Registration ActionBy { get; set; }

        /// <value>long</value>
        [Required]
        public long RolePermissionId { get; set; }
        /// <value>RolePermission</value>
        [ForeignKey("RolePermissionId")]
        public RolePermission RolePermission { get; set; }

        /// <value>long</value>
        [Required]
        public long StatusId { get; set; }
        /// <value>Permission</value>
        [ForeignKey("StatusId")]
        public Status Status { get; set; }

        /// <value>string</value>
        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        #region Instance Methods

        #endregion

        #region Static Methods

        #endregion
    }
}
