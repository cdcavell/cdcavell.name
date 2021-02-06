namespace as_api_cdcavell.Models.AppSettings
{
    /// <summary>
    /// ConnectionStrings model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 02/05/2021 | Initial build Authorization Service |~ 
    /// </revision>
    public class ConnectionStrings
    {
        /// <value>string</value>
        public string AuthorizationServiceConnection { get; set; }
        /// <value>string</value>
        public string RedisCacheConnection { get; set; }
    }
}
