using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace is4_cdcavell.Models.AppSettings
{
    /// <summary>
    /// Facebook Authentication model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 09/30/2020 | Initial build |~ 
    /// </revision>
    public class Facebook
    {
        /// <value>string</value>
        public string AppId { get; set; }
        /// <value>string</value>
        public string AppSecret { get; set; }
    }
}
