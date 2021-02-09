using Microsoft.Extensions.DependencyInjection;
using System;

namespace cdcavell.Services.Authorization
{
    /// <summary>
    /// Application Authorization Web Service Options Extension
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.1 | 02/09/2021 | User Authorization Web Service |~ 
    /// </revision>
    public static class ApplicationAuthorizationServiceExtension
    {
        /// <summary>
        /// Add Application Authorization Web Service Options Extention
        /// </summary>
        /// <param name="serviceCollection">IServiceCollection</param>
        /// <param name="options">Action&lt;UserAuthorizationServiceOptions&gt;</param>
        /// <method>AddUserAuthorizationService(this IServiceCollection serviceCollection, Action&lt;UserAuthorizationServiceOptions&gt; options)</method>
        public static IServiceCollection AddUserAuthorizationService(this IServiceCollection serviceCollection, Action<UserAuthorizationServiceOptions> options)
        {
            serviceCollection.AddScoped<IApplicationAuthorizationService, ApplicationAuthorizationService>();
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), @"Missing required options for UserAuthorizationService.");
            }
            serviceCollection.Configure(options);
            return serviceCollection;
        }
    }
}
