using CDCavell.ClassLibrary.Web.Services.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

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
    /// | Christopher D. Cavell | 1.0.3.3 | 03/09/2021 | User Authorization Web Service |~ 
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
        /// <value>DateTime?</value>
        [AllowNull]
        [DataType(DataType.DateTime)]
        [Display(Name = "Request Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? RequestDate { get; set; } = DateTime.MinValue;

        /// <value>string</value>
        [AllowNull]
        [DataType(DataType.Text)]
        [Display(Name = "Status")]
        public string Status { get; set; }

        /// <value>bool</value>
        public bool PendingValidation { get; set; }

        /// <value>List&lt;RolePermissionModel&gt;</value>
        public List<RolePermissionModel> RolePermissions { get; set; }
    }
}
