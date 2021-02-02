using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CDCavell.ClassLibrary.Web.Mvc.Models.Authorization
{
    /// <summary>
    /// Application model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 02/01/2021 | Initial build Authorization Service |~ 
    /// </revision>
    public class RegistrationCheck
    {
        /// <value>bool</value>
        public bool IsRegistered { get; set; }
        /// <value>string</value>
        public string Email { get; set; }
    }
}
