namespace CDCavell.ClassLibrary.Web.Services.AppSettings
{
    /// <summary>
    /// AppSettings Web Service Interface
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.1 | 02/09/2021 | User Authorization Web Service |~ 
    /// </revision>
    public interface IAppSettingsService
    {
        /// <summary>
        /// Get AssemblyName value
        /// </summary>
        /// <returns>string</returns>
        string AssemblyName();

        /// <summary>
        /// Get AssemblyVersion value
        /// </summary>
        /// <returns>string</returns>
        public string AssemblyVersion();

        /// <summary>
        /// Get LastModifiedDate value
        /// </summary>
        /// <returns>string</returns>
        public string LastModifiedDate();

        /// <summary>
        /// Get main site url value
        /// </summary>
        /// <returns>string</returns>
        string MainUrl();

        /// <summary>
        /// Get authorization ui site url value
        /// </summary>
        /// <returns>string</returns>
        string AuthorizationUrl();
    }
}
