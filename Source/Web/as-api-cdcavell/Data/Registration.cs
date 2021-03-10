using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace as_api_cdcavell.Data
{
    /// <summary>
    /// Registration Entity
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 02/01/2021 | Initial build Authorization Service |~ 
    /// | Christopher D. Cavell | 1.0.3.3 | 03/09/2021 | User Authorization Service |~ 
    /// </revision>
    [Table("Registration")]
    public class Registration : DataModel<Registration>
    {
        /// <value>string</value>
        [Required]
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
        [AllowNull]
        [DataType(DataType.Text)]
        [Display(Name = "Validation Token")]
        public string ValidationToken { get; set; } = Guid.NewGuid().ToString();
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
        /// <value>DateTime?</value>
        [AllowNull]
        [DataType(DataType.DateTime)]
        [Display(Name = "Approved Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? ApprovedDate { get; set; } = DateTime.MinValue;

        /// <value>long?</value>
        [AllowNull]
        public long? ApprovedById { get; set; }
        /// <value>Registration</value>
        [ForeignKey("ApprovedById")]
        public virtual Registration ApprovedBy { get; set; }

        /// <value>DateTime?</value>
        [AllowNull]
        [DataType(DataType.DateTime)]
        [Display(Name = "Revoked Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? RevokedDate { get; set; } = DateTime.MinValue;

        /// <value>long?</value>
        [AllowNull]
        public long? RevokedById { get; set; }
        /// <value>Registration</value>
        [ForeignKey("RevokedById")]
        public virtual Registration RevokedBy { get; set; }

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
        /// <value>bool</value>
        [NotMapped]
        public bool PendingValidation
        {
            get
            {
                if (RequestDate > (DateTime?)DateTime.MinValue)
                    if (ValidationDate == (DateTime?)DateTime.MinValue)
                        if (ApprovedDate == (DateTime?)DateTime.MinValue)
                            if (RevokedDate == (DateTime?)DateTime.MinValue)
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

                if (PendingValidation)
                    return "Pending Validation";
                            
                return "Not Registered";
            }
        }

        /// <value>List&lt;RolePermission&gt;</value>
        public List<RolePermission> RolePermissions { get; set; }


        #region Instance Methods

        #endregion

        #region Static Methods

        /// <summary>
        /// Get registration for given email address
        /// </summary>
        /// <param name="emailAddress">string</param>
        /// <param name="dbContext">CDCavellDbContext</param>
        /// <returns>Registration</returns>
        /// <method>Get(string emailAddress, AuthorizationServiceDbContext dbContext)</method>
        public static Registration Get(string emailAddress, AuthorizationServiceDbContext dbContext)
        {
            Registration registration = dbContext.Registration
                .Where(x => x.Email == emailAddress.Trim().Clean())
                .FirstOrDefault();

            if (registration == null)
                registration = new Registration();

            return registration;
        }

        #endregion
    }
}
