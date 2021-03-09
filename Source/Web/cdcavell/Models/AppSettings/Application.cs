namespace cdcavell.Models.AppSettings
{
    /// <summary>
    /// Application model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.3 | 03/08/2021 | User Authorization Web Service |~
    /// </revision>
    public class Application
    {
        /// <value>ConnectionStrings</value>
        public ConnectionStrings ConnectionStrings { get; set; }
        /// <value>BingWebSearchModel</value>
        public BingWebSearchModel BingWebSearch { get; set; }
        /// <value>BingWebmasterModel</value>
        public BingWebmasterModel BingWebmaster { get; set; }
    }
}
