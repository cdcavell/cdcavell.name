using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace cdcavell.Data
{
    /// <summary>
    /// CDCavell Registration Entity
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.9 | 11/03/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// </revision>
    [Table("Registration")]
    public class Registration : DataModel<Registration>
    {
        /// <value>string</value>
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        /// <value>string</value>
        [Required]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        /// <value>string</value>
        [Required]
        [DataType(DataType.Text)]
        public string LastName { get; set; }


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

        #endregion
    }
}
