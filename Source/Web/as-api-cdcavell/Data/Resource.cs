using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace as_api_cdcavell.Data
{
    /// <summary>
    /// Resource Entity
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/20/2021 | Initial build Authorization Service |~ 
    /// </revision>
    [Table("Resource")]
    public class Resource : DataModel<Role>
    {
        /// <value>string</value>
        [Required]
        [DataType(DataType.Text)]
        public string ClientId { get; set; }

        /// <value>string</value>
        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }


        /// <value>List&lt;Role&gt;</value>
        public List<Role> Roles { get; set; }


        #region Instance Methods

        #endregion

        #region Static Methods

        #endregion
    }
}
