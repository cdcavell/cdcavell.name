using CDCavell.ClassLibrary.Commons.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CDCavell.ClassLibrary.Web.Services.AppSettings
{
    /// <summary>
    /// AppSettings Web Service
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.3 | 03/09/2021 | User Authorization Web Service |~ 
    /// </revision>
    public class AppSettingsService : IAppSettingsService
    {
        private readonly Logger _logger;
        private readonly Mvc.Models.AppSettings.AppSettings _appSettings;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">ILogger&lt;AppSettingsService&gt;</param>
        /// <param name="options">IOptions&lt;AppSettingsServiceOptions&gt;</param>
        /// <method>UserAuthorizationService(ILogger&lt;UserAuthorizationService&gt; logger, IOptions&lt;UserAuthorizationServiceOptions&gt; options)</method>
        public AppSettingsService(ILogger<AppSettingsService> logger, IOptions<AppSettingsServiceOptions> options)
        {
            _logger = new Logger(logger);
            _appSettings = (Mvc.Models.AppSettings.AppSettings)options.Value.AppSettings;
        }

        /// <summary>
        /// Get AssemblyName value
        /// </summary>
        /// <returns>string</returns>
        public string AssemblyName()
        {
            return _appSettings.AssemblyName;
        }

        /// <summary>
        /// Get AssemblyVersion value
        /// </summary>
        /// <returns>string</returns>
        public string AssemblyVersion()
        {
            return _appSettings.AssemblyVersion;
        }

        /// <summary>
        /// Get LastModifiedDate value
        /// </summary>
        /// <returns>string</returns>
        public string LastModifiedDate()
        {
            return _appSettings.LastModifiedDate.ToString("MM/dd/yyyy");
        }

        /// <summary>
        /// Get main site url value
        /// </summary>
        /// <returns>string</returns>
        public string MainUrl()
        {
            return _appSettings.Authorization.AuthorizationService.MainTrim;
        }

        /// <summary>
        /// Get authorization ui site url value
        /// </summary>
        /// <returns>string</returns>
        public string AuthorizationUrl()
        {
            return _appSettings.Authorization.AuthorizationService.UiTrim;
        }
    }
}
