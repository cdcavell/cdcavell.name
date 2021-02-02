using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace as_api_cdcavell.Data
{
    /// <summary>
    /// Status Entity
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/20/2021 | Initial build Authorization Service |~ 
    /// </revision>
    [Table("Status")]
    public class Status : DataModel<Status>
    {
        /// <value>string</value>
        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }


        /// <value>List&lt;RolePermission&gt;</value>
        public List<RolePermission> RolePermissions { get; set; }

        #region Instance Methods

        #endregion

        #region Static Methods

        #endregion
    }
}
