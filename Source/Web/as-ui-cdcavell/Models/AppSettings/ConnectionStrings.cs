namespace as_ui_cdcavell.Models.AppSettings
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
    /// | Christopher D. Cavell | 1.0.3.3 | 03/07/2021 | User Authorization Web Service |~
    /// </revision>
    public class ConnectionStrings
    {
        /// <value>string</value>
        public string AuthorizationConnection { get; set; }
        /// <value>string</value>
        public string ApplicationInsightsConnection { get; set; }
    }
}
