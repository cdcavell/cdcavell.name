namespace is4_cdcavell.Models.AppSettings
{
    /// <summary>
    /// Application model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 09/28/2020 | Initial build |~ 
    /// </revision>
    public class Application
    {
        /// <value>string</value>
        public string Name { get; set; }
        /// <value>string</value>
        public string Version { get; set; }
        /// <value>string</value>
        public string SecretKey { get; set; }
    }
}
