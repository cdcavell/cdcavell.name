using System;
using System.IO;
using System.Reflection;

namespace dis5_cdcavell.Models.AppSettings
{
    /// <summary>
    /// AppSettings model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.2.0 | 01/16/2021 | Initial build |~ 
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
