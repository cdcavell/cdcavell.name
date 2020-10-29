using System.Reflection;

namespace cdcavell.Models.AppSettings
{
    /// <summary>
    /// AppSettings model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 10/20/2020 | Initial build |~ 
    /// | Christopher D. Cavell | 1.0.1 | 10/28/2020 | Add YouTubeVideos |~ 
    /// | Christopher D. Cavell | 1.0.1 | 10/29/2020 | Remove YouTubeVideos (Not Implemented) |~ 
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
        /// <value>Authentication</value>
        public Authentication Authentication { get; set; }
        /// <value>Authorization</value>
        public Authorization Authorization { get; set; }
        /// <value>Application</value>
        public Application Application { get; set; }
    }
}