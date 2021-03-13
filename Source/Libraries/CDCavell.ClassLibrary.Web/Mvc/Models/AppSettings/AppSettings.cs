using System;
using System.IO;
using System.Reflection;

namespace CDCavell.ClassLibrary.Web.Mvc.Models.AppSettings
{
    /// <summary>
    /// AppSettings model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.3 | 03/13/2021 | User Authorization Service |~ 
    /// </revision>
    public abstract partial class AppSettings
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
        /// <value>string</value>
        public string SecretKey { get; set; }
        /// <value>SiteAdministrator</value>
        public SiteAdministrator SiteAdministrator { get; set; }
        /// <value>Authentication</value>
        public Authentication Authentication { get; set; }
        /// <value>Authorization</value>
        public Authorization Authorization { get; set; }
        /// <value>ConnectionStrings</value>
        public ConnectionStrings ConnectionStrings { get; set; }
        /// <value>EmailService</value>
        public EmailService EmailService { get; set; }

    }
}