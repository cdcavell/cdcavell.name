using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace is4_cdcavell.Models.AppSettings
{
    /// <summary>
    /// Authentication model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 10/09/2020 | Initial build |~ 
    /// </revision>
    public class Authentication
    {
        /// <value>Twitter</value>
        public Twitter Twitter { get; set; }
        /// <value>Facebook</value>
        public Facebook Facebook { get; set; }
        /// <value>Microsoft</value>
        public Microsoft Microsoft { get; set; }
    }
}
