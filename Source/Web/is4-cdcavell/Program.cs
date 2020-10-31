using CDCavell.ClassLibrary.Commons.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading;

namespace is4_cdcavell
{
    /// <summary>
    /// Entry point class
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 09/28/2020 | Initial build |~ 
    /// </revision>
    public class Program
    {
        private static string _assemblyName;
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
            _assemblyName = Assembly.GetEntryAssembly().GetName().Name;

            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            _logger = new Logger(serviceProvider.GetService<ILogger<Program>>());

            try
            {
                _logger.Information($"{_assemblyName} Started");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                _logger.Exception(e, $"{_assemblyName} Exception Error");
            }
            finally
            {
                _logger.Information($"{_assemblyName} Ended");
                Thread.Sleep(1000);
                serviceProvider.Dispose();
            }
        }

        /// <summary>
        /// Host Builder configuration
        /// </summary>
        /// <param name="args">string[]</param>
        /// <returns>IHostBuilder</returns>
        /// <method>CreateHostBuilder(string[] args)</method>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    logging.AddConsole();

                    // Providing an instrumentation key here is required if you're using
                    // standalone package Microsoft.Extensions.Logging.ApplicationInsights
                    // or if you want to capture logs from early in the application startup 
                    // pipeline from Startup.cs or Program.cs itself.
                    logging.AddApplicationInsights("ikey");
                    // Adding the filter below to ensure logs of all severity from Program.cs
                    // is sent to ApplicationInsights.
                    logging.AddFilter<Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider>
                                     (typeof(Program).FullName, LogLevel.Trace);

                    // Adding the filter below to ensure logs of all severity from Startup.cs
                    // is sent to ApplicationInsights.
                    logging.AddFilter<Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider>
                                     (typeof(Startup).FullName, LogLevel.Trace);

                    logging.AddEventLog(eventLogSettings =>
                    {
                        eventLogSettings.SourceName = _assemblyName;
                        eventLogSettings.LogName = "Application";
                    });

                    if (Equals(_environmentName, "development"))
                        logging.AddDebug();
                })
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.AddJsonFile("appsettings.json", optional: true);
                    configApp.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: false);
                    configApp.AddEnvironmentVariables();
                    configApp.AddUserSecrets(Assembly.GetEntryAssembly(), optional: true);
                    configApp.AddCommandLine(args);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.CaptureStartupErrors(true);
                    webBuilder.UseSetting("detailedErrors", "true");
                    webBuilder.UseStaticWebAssets();
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
