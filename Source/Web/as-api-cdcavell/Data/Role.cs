using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace as_api_cdcavell.Data
{
    /// <summary>
    /// Role Entity
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/20/2021 | Initial build Authorization Service |~ 
    /// </revision>
    [Table("Role")]
    public class Role : DataModel<Role>
    {
        /// <value>string</value>
        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        /// <value>long</value>
        [Required]
        public long ResourceId { get; set; }
        /// <value>Registration</value>
        [ForeignKey("ResourceId")]
        public Registration Resource { get; set; }


        /// <value>List&lt;Permission&gt;</value>
        public List<Permission> Permissions { get; set; }


        #region Instance Methods

        #endregion

        #region Static Methods

        #endregion
    }
}
