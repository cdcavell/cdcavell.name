using System.ComponentModel.DataAnnotations;

namespace CDCavell.ClassLibrary.Web.Services.Authorization
{
    /// <summary>
    /// Status Web Service Model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.3 | 02/27/2021 | User Authorization Service |~ 
    /// </revision>
    public class StatusModel
    {
        /// <value>long</value>
        [Display(Name = "Status Id")]
        public long StatusId { get; set; }

        /// <value>string</value>
        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }
    }
}
