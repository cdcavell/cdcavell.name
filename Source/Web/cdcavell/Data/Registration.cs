using Org.BouncyCastle.Asn1.Esf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

namespace cdcavell.Data
{
    /// <summary>
    /// CDCavell Registration Entity
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.9 | 11/11/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// </revision>
    [Table("Registration")]
    public class Registration : DataModel<Registration>
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
        public string ApprovedBy { get; set; }
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
        public string RevokedBy { get; set; }
        /// <value>bool</value>
        [NotMapped]
        public bool IsActive
        {
            get
            {
                if (ApprovedDate != DateTime.MinValue)
                    if (RevokedDate == DateTime.MinValue)
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
                    return "Active";

                if (IsRevoked)
                    return "Revoked";

                return "Pending";
            }
        }

        /// <value>ICollection&lt;Role&gt;</value>
        public List<RolePermission> RolePermissions { get; set; }


        #region Instance Methods

        #endregion

        #region Static Methods

        /// <summary>
        /// Is email addressed registered
        /// </summary>
        /// <param name="emailAddress">string</param>
        /// <param name="dbContext">CDCavellDbContext</param>
        /// <returns>bool</returns>
        /// <method>IsRegistered(string emailAddress, CDCavellDbContext dbContext)</method>
        public static bool IsRegistered(string emailAddress, CDCavellDbContext dbContext)
        {
            int count = dbContext.Registration
                .Where(x => x.Email == emailAddress.Trim().Clean())
                .Count();

            return count > 0 ? true : false;
        }

        /// <summary>
        /// Get registration for given email address
        /// </summary>
        /// <param name="emailAddress">string</param>
        /// <param name="dbContext">CDCavellDbContext</param>
        /// <returns>Registration</returns>
        /// <method>Get(string emailAddress, CDCavellDbContext dbContext)</method>
        public static Registration Get(string emailAddress, CDCavellDbContext dbContext)
        {
            return dbContext.Registration
                .Where(x => x.Email == emailAddress.Trim().Clean())
                .FirstOrDefault();
        }

        #endregion
    }
}
