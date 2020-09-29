﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace is4_cdcavell.Models.AppSettings
{
    /// <summary>
    /// AppSettings model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 09/28/2020 | Initial build |~ 
    /// </revision>
    public class AppSettings
    {
        /// <value>string</value>
        public string AssemblyName 
        { 
            get { return Assembly.GetEntryAssembly().GetName().Name; }
        }
        /// <value>Application</value>
        public Application Application { get; set; }
    }
}
