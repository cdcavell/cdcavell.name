using System;
using System.Collections.Generic;

namespace CDCavell.ClassLibrary.Web.Services.Authorization
{
    /// <summary>
    /// User Authorization Web Service Model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.1 | 02/08/2021 | User Authorization Web Service |~ 
    /// | Christopher D. Cavell | 1.0.3.3 | 02/27/2021 | User Authorization Web Service |~ 
    /// </revision>
    public class UserAuthorizationModel
    {
        /// <value>string</value>
        public string ClientId { get; set; }
        /// <value>string</value>
        public string IdentityProvider { get; set; }
        /// <value>string</value>
        public DateTime DateTimeRequsted { get; set; }
        /// <value>string</value>
        public string Email 
        { 
            get { return Registration.Email; } 
        }
        /// <value>RegistrationModel</value>
        public RegistrationModel Registration { get; set; }
        /// <value>List&lt;RolePermission&gt;</value>
        public List<RolePermissionModel> RolePermissions { get; set; }

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <method></method>
        public UserAuthorizationModel()
        {
            this.Registration = new RegistrationModel();
            this.RolePermissions = new List<RolePermissionModel>();
        }
    }
}
