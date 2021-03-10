using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CDCavell.ClassLibrary.Web.Services.Authorization
{
    /// <summary>
    /// Registration Web Service Model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.1 | 02/07/2021 | User Authorization Web Service |~ 
    /// | Christopher D. Cavell | 1.0.3.3 | 03/09/2021 | User Authorization Web Service |~ 
    /// </revision>
    public class RegistrationModel
    {
        /// <value>long</value>
        [Display(Name = "Registration Id")]
        public long RegistrationId { get; set; }
        /// <value>string</value>
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        /// <value>DateTime?</value>
        [AllowNull]
        [DataType(DataType.DateTime)]
        [Display(Name = "Validation Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? ValidationDate { get; set; } = DateTime.MinValue;
        /// <value>string</value>
        [DataType(DataType.Text)]
        [Display(Name = "Validation Token")]
        public string ValidationToken { get; set; } = Guid.NewGuid().ToString();
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
                if (RequestDate > (DateTime?)DateTime.MinValue)
                    if (!string.IsNullOrEmpty(Email))
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
                if (RequestDate > (DateTime?)DateTime.MinValue)
                    if (ValidationDate > (DateTime?)DateTime.MinValue)
                        if (ApprovedDate > (DateTime?)DateTime.MinValue)
                            if (RevokedDate == (DateTime?)DateTime.MinValue)
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
                if (RequestDate > (DateTime?)DateTime.MinValue)
                    if (ValidationDate > (DateTime?)DateTime.MinValue)
                        if (ApprovedDate == (DateTime?)DateTime.MinValue)
                            if (RevokedDate == (DateTime?)DateTime.MinValue)
                                if (!string.IsNullOrEmpty(Email))
                                    return true;

                return false;
            }
        }
        /// <value>bool</value>
        [NotMapped]
        public bool IsValidated
        {
            get
            {
                if (RequestDate > (DateTime?)DateTime.MinValue)
                    if (ValidationDate > (DateTime?)DateTime.MinValue)
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
                if (RevokedDate > (DateTime?)DateTime.MinValue)
                    if (!string.IsNullOrEmpty(Email))
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

                if (!IsValidated)
                    if (IsRegistered)
                        return "Pending Validation";

                return "Not Registered";
            }
        }
    }
}
