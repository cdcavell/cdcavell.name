using System;
using System.Collections.Generic;
using System.IO;
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
    /// | Christopher D. Cavell | 1.0.0.0 | 10/20/2020 | Initial build |~ 
    /// | Christopher D. Cavell | 1.0.0.9 | 11/12/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// </revision>
    public class AppSettings
    {
        /// <value>string</value>
        public string AssemblyName 
        { 
            get { return Assembly.GetEntryAssembly().GetName().Name; }
        }
        /// <value>string</value>
        public string AssemblyVersion
        {
            get { return Assembly.GetEntryAssembly().GetName().Version.ToString(); }
        }
        /// <value>DateTime</value>
        public DateTime LastModifiedDate
        {
            get { return File.GetLastWriteTime(Assembly.GetEntryAssembly().Location); }
        }
        /// <value>ConnectionStrings</value>
        public ConnectionStrings ConnectionStrings { get; set; }
        /// <value>Authentication</value>
        public Authentication Authentication { get; set; }
        /// <value>Application</value>
        public Application Application { get; set; }
    }
}
