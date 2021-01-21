using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace as_api_cdcavell.Data
{
    /// <summary>
    /// History Entity
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/20/2021 | Initial build Authorization Service |~ 
    /// </revision>
    [Table("History")]
    public class History : DataModel<History>
    {
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
