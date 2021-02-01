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
    /// | Christopher D. Cavell | 1.0.3.0 | 02/01/2021 | Initial build Authorization Service |~ 
    /// </revision>
    public class UserAuthorization
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
        /// <value>Registration</value>
        public Registration Registration { get; set; }

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <method></method>
        public UserAuthorization()
        {
            this.Registration = new Authorization.Registration();
        }
    }
}
