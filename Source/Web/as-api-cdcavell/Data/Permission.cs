using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace as_api_cdcavell.Data
{
    /// <summary>
    /// Permission Entity
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/20/2021 | Initial build Authorization Service |~ 
    /// </revision>
    [Table("Permission")]
    public class Permission : DataModel<Permission>
    {
        /// <value>string</value>
        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }


        /// <value>long</value>
        [Required]
        public long RoleId { get; set; }
        /// <value>Role</value>
        [ForeignKey("RoleId")]
        public Role Role { get; set; }


        #region Instance Methods

        #endregion

        #region Static Methods

        #endregion
    }
}
