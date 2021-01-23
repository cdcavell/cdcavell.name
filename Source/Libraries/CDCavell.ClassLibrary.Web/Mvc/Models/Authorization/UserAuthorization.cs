using System;

namespace CDCavell.ClassLibrary.Web.Mvc.Models.Authorization
{
    /// <summary>
    /// User Authorization global model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/23/2021 | Initial build Authorization Service |~ 
    /// </revision>
    public class UserAuthorization
    {
        /// <value>long</value>
        public long RegistrationId { get; set; }
        /// <value>string</value>
        public string RegistrationStatus { get; set; }
        /// <value>string</value>
        public string ClientId { get; set; }
        /// <value>string</value>
        public string IdentityProvider { get; set; }
        /// <value>string</value>
        public DateTime DateTimeRequsted { get; set; } 
        /// <value>string</value>
        public string Email { get; set; }
        /// <value>string</value>
        public string FirstName { get; set; }
        /// <value>string</value>
        public string LastName { get; set; }

        /// <value>bool</value>
        public bool IsNew
        {
            get
            {
                if (this.RegistrationId == 0)
                    return true;

                return false;
            }
        }
    }
}
