using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDCavell.ClassLibrary.Web.Services.Authorization
{
    /// <summary>
    /// Role Web Service Model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.3 | 02/27/2021 | User Authorization Service |~ 
    /// </revision>
    [Table("Role")]
    public class RoleModel
    {
        /// <value>long</value>
        [Required]
        [Display(Name = "Role Id")]
        public long RoleId { get; set; }

        /// <value>string</value>
        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        /// <value>long</value>
        [Required]
        public long ResourceId { get; set; }
        /// <value>Resource</value>
        public ResourceModel Resource { get; set; }
    }
}
