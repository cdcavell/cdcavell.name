using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CDCavell.ClassLibrary.Web.Mvc.Models.Authorization
{
    /// <summary>
    /// Registration model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 02/01/2021 | Initial build Authorization Service |~ 
    /// </revision>
    public class Registration
    {
        /// <value>long</value>
        [Display(Name = "Registration Id")]
        public long RegistrationId { get; set; }
        /// <value>string</value>
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        /// <value>string</value>
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        /// <value>string</value>
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        /// <value>DateTime?</value>
        [AllowNull]
        [DataType(DataType.DateTime)]
        [Display(Name = "Request Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? RequestDate { get; set; } = DateTime.MinValue;
        /// <value>DateTime?</value>
        [AllowNull]
        [DataType(DataType.DateTime)]
        [Display(Name = "Approved Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? ApprovedDate { get; set; } = DateTime.MinValue;
        /// <value>string</value>
        [AllowNull]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Approved By")]
        public string ApprovedBy { get; set; } = string.Empty;
        /// <value>DateTime?</value>
        [AllowNull]
        [DataType(DataType.DateTime)]
        [Display(Name = "Revoked Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? RevokedDate { get; set; } = DateTime.MinValue;
        /// <value>string</value>
        [AllowNull]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Revoked By")]
        public string RevokedBy { get; set; } = string.Empty;
        /// <value>bool</value>
        [NotMapped]
        public bool IsRegistered
        {
            get 
            {
                if (IsActive || IsPending || IsRevoked)
                    return true;

                return false;
            }
        }
        /// <value>bool</value>
        [NotMapped]
        public bool IsActive
        {
            get
            {
                if (ApprovedDate != DateTime.MinValue)
                    if (RevokedDate == DateTime.MinValue)
                        if (!string.IsNullOrEmpty(Email))
                            return true;

                return false;
            }
        }
        /// <value>bool</value>
        [NotMapped]
        public bool IsPending
        {
            get
            {
                if (ApprovedDate == DateTime.MinValue)
                    if (RevokedDate == DateTime.MinValue)
                        if (RequestDate > DateTime.MinValue)
                            if (!string.IsNullOrEmpty(Email))
                                return true;

                return false;
            }
        }
        /// <value>bool</value>
        [NotMapped]
        public bool IsRevoked
        {
            get
            {
                if (RevokedDate != DateTime.MinValue)
                    return true;

                return false;
            }
        }
        /// <value>string</value>
        [NotMapped]
        public string Status
        {
            get 
            {
                if (IsActive)
                    return "Account Active";

                if (IsRevoked)
                    return "Account Revoked";

                if (IsPending)
                    return "Pending Approval";

                return "Not Registered";
            }
        }
    }
}
