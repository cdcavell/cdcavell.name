using Microsoft.Extensions.DependencyInjection;
using System;

namespace CDCavell.ClassLibrary.Web.Services.AppSettings
{
    /// <summary>
    /// AppSettings Web Service Options Extension
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.3 | 03/09/2021 | User Authorization Web Service |~ 
    /// </revision>
    public static class AppSettingsServiceOptionsExtention
    {
        /// <summary>
        /// Add AppSettings Web Service Options Extention
        /// </summary>
        /// <param name="serviceCollection">IServiceCollection</param>
        /// <param name="options">Action&lt;UserAuthorizationServiceOptions&gt;</param>
        /// <method>AddAppSettingsService(this IServiceCollection serviceCollection, Action&lt;UserAuthorizationServiceOptions&gt; options)</method>
        public static IServiceCollection AddAppSettingsService(this IServiceCollection serviceCollection, Action<AppSettingsServiceOptions> options)
        {
            serviceCollection.AddScoped<IAppSettingsService, AppSettingsService>();
            if (options == null)
                throw new ArgumentNullException(nameof(options), @"Missing required options for AppSettingsService.");

            serviceCollection.Configure(options);
            return serviceCollection;
        }
    }
}
