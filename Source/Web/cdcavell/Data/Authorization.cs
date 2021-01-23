using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cdcavell.Data
{
    /// <summary>
    /// CDCavell SiteMap Entity
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 10/23/2020 | Initial build Authorization Service |~ 
    /// </revision>
    [Table("Authorization")]
    public class Authorization : DataModel<Authorization>
    {
        /// <value>string</value>
        [Required]
        [DataType(DataType.Text)]
        public string Guid { get; set; }
        /// <value>string</value>
        [Required]
        [DataType(DataType.Text)]
        public string Object { get; set; }
    }
}
