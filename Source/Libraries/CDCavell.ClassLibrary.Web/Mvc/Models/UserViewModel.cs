using System.Collections.Generic;
using System.Net;

namespace CDCavell.ClassLibrary.Web.Mvc.Models
{
    /// <summary>
    /// User view model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 09/29/2020 | Initial build |~ 
    /// </revision>
    public class UserViewModel
    {
        /// <value>string</value>
        public bool IsAuthenticated { get; set; }
        /// <value>string</value>
        public string Id { get; set; }
        /// <value>string</value>
        public string Name { get; set; }
        /// <value>string</value>
        public string Email { get; set; }
        /// <value>List&lt;string&gt;</value>
        public List<string> Roles { get; set; }
        /// <value>IPAddress</value>
        public IPAddress IPAddress { get; set; }
    }
}
