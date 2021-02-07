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
    /// | Christopher D. Cavell | 1.0.3.1 | 02/07/2021 | Add ApplicationInsights |~
    /// </revision>
    public class ConnectionStrings
    {
        /// <value>string</value>
        public string AuthorizationServiceConnection { get; set; }
        /// <value>string</value>
        public string ApplicationInsightsConnection { get; set; }
    }
}
