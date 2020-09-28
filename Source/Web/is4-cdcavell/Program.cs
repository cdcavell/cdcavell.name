using CDCavell.ClassLibrary.Commons.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace is4_cdcavell
{
    /// <summary>
    /// Entry point class
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 09/28/2020 | Initial build |~ 
    /// </revision>
    public class Program
    {
        private static string _environmentName;
        private static Logger _logger;

        /// <summary>
        /// Entry point method
        /// </summary>
        /// <param name="args">string[]</param>
        /// <method>Main(string[] args)</method>
        public static void Main(string[] args)
        {
            _environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLower();

            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            _logger = new Logger(serviceProvider.GetService<ILogger<Program>>());

            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Host Builder configuration
        /// </summary>
        /// <param name="args">string[]</param>
        /// <returns>IHostBuilder</returns>
        /// <method>CreateHostBuilder(string[] args)</method>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void ConfigureServices(ServiceCollection services)
        {
            if (Equals(_environmentName, "development"))
                services.AddLogging(configure => configure.AddDebug());
            else
                services.AddLogging(configure => configure.AddConsole());
        }
    }
}
