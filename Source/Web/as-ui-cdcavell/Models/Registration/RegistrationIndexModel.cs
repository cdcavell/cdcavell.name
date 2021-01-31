using System.ComponentModel.DataAnnotations;

namespace as_ui_cdcavell.Models.Registration
{
    /// <summary>
    /// Index model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/31/2021 | Initial build Authorization Service |~ 
    /// </revision>    
    public class RegistrationIndexModel
    {
        /// <value>string</value>
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        /// <value>string</value>
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        /// <value>string</value>
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}
