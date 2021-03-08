namespace cdcavell.Models.AppSettings
{
    /// <summary>
    /// AppSettings model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 10/20/2020 | Initial build |~ 
    /// | Christopher D. Cavell | 1.0.0.1 | 10/28/2020 | Add YouTubeVideos |~ 
    /// | Christopher D. Cavell | 1.0.0.1 | 10/29/2020 | Remove YouTubeVideos (Not Implemented) |~ 
    /// | Christopher D. Cavell | 1.0.0.7 | 10/31/2020 | Integrate Bing’s Adaptive URL submission API with your website [#144](https://github.com/cdcavell/cdcavell.name/issues/144) |~ 
    /// | Christopher D. Cavell | 1.0.0.9 | 11/12/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// | Christopher D. Cavell | 1.0.3.3 | 03/08/2021 | User Authorization Web Service |~
    /// </revision>
    public class AppSettings : CDCavell.ClassLibrary.Web.Mvc.Models.AppSettings.AppSettings
    {
        /// <value>Application</value>
        public Application Application { get; set; }
    }
}