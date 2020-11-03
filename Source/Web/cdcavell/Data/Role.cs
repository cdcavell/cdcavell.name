using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cdcavell.Data
{
    /// <summary>
    /// CDCavell Roles Entity
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.9 | 11/03/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// </revision>
    [Table("Role")]
    public class Role : DataModel<Role>
    {
        /// <value>string</value>
        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }


        /// <value>List&lt;Permission&gt;</value>
        public List<Permission> Permissions { get; set; }


        #region Instance Methods

        #endregion

        #region Static Methods

        #endregion
    }
}
