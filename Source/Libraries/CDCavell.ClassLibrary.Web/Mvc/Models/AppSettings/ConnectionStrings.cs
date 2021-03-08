namespace CDCavell.ClassLibrary.Web.Mvc.Models.AppSettings
{
    /// <summary>
    /// ConnectionStrings model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.3 | 03/07/2021 | User Authorization Service |~ 
    /// </revision>
    public abstract partial class ConnectionStrings
    {
        /// <value>string</value>
        public string AuthorizationConnection { get; set; }
        /// <value>string</value>
        public string ApplicationInsightsConnection { get; set; }
    }
}
