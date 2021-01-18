using System;
using System.IO;
using System.Reflection;

namespace as_cdcavell.Models.AppSettings
{
    /// <summary>
    /// AppSettings model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/18/2021 | Initial build Authorization Service |~ 
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
        /// <value>Authentication</value>
        public Authentication Authentication { get; set; }
        /// <value>Authorization</value>
        public Application Application { get; set; }
        /// <value>ConnectionStrings</value>
        public ConnectionStrings ConnectionStrings { get; set; }
    }
}