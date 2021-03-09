using CDCavell.ClassLibrary.Commons.Logging;
using CDCavell.ClassLibrary.Web.Mvc.Filters;
using CDCavell.ClassLibrary.Web.Security;
using dis5_cdcavell.Data;
using dis5_cdcavell.Filters;
using dis5_cdcavell.Models.Account;
using dis5_cdcavell.Models.AppSettings;
using Duende.IdentityServer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Reflection;

namespace dis5_cdcavell
{
    /// <summary>
    /// The Startup class configures services and the application's request pipeline&lt;br /&gt;&lt;br /&gt;
    /// _Services_ are components that are used by the app. For example, a logging component is a service. Code to configure (_or register_) services is added to the ```Startup.ConfigureServices``` method.&lt;br /&gt;&lt;br /&gt;
    /// The request handling pipeline is composed as a series of _middleware_ components. For example, a middleware might handle requests for static files or redirect HTTP requests to HTTPS. Each middleware performs asynchronous operations on an ```HttpContext``` and then either invokes the next middleware in the pipeline or terminates the request. Code to configure the request handling pipeline is added to the ```Startup.Configure``` method.
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.2.0 | 01/16/2021 | Initial build |~ 
    /// | Christopher D. Cavell | 1.0.3.0 | 02/06/2021 | Initial build Authorization Service |~ 
    /// | Christopher D. Cavell | 1.0.3.1 | 02/07/2021 | Utilize Redis Cache - Not implemented |~
    /// | Christopher D. Cavell | 1.0.3.3 | 03/08/2021 | User Authorization Web Service |~ 
    /// </revision>
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private Logger _logger;
        private AppSettings _appSettings;

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="configuration">IConfiguration</param>
        /// <param name="webHostEnvironment">IWebHostEnvironment</param>
        /// <method>Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)</method>
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// This optional method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <method>ConfigureServices(IServiceCollection services)</method>
        public void ConfigureServices(IServiceCollection services)
        {
            // Register appsettings.json
            AppSettings appSettings = new AppSettings();
            _configuration.Bind("AppSettings", appSettings);
            _appSettings = appSettings;
            services.AddSingleton(_appSettings);

            services.AddMvc();
            services.AddControllersWithViews();

            // Register IHttpContextAccessor
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Register controller fillters
            services.AddScoped<SecurityHeadersAttribute>();
            services.AddScoped<ControllerActionLogFilter>();
            services.AddScoped<ControllerActionUserFilter>();
            services.AddScoped<ControllerActionPageFilter>();

            // Register RsaKeyService
            var rsa = new RsaKeyService(_webHostEnvironment, TimeSpan.FromDays(30));
            services.AddSingleton<RsaKeyService>(provider => rsa);

            services.AddDbContext<AspIdUsersDbContext>(options =>
                options.UseSqlite(appSettings.ConnectionStrings.AspIdUsersConnection));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AspIdUsersDbContext>()
                .AddDefaultTokenProviders();

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                options.EmitStaticAudienceClaim = true;
            })
                .AddAspNetIdentity<ApplicationUser>();

            // in-memory, code config
            builder.AddInMemoryIdentityResources(Config.IdentityResources);
            builder.AddInMemoryApiScopes(Config.ApiScopes);
            builder.AddInMemoryClients(Config.Clients);

            // not recommended for production - you need to store your key material somewhere secure
            // builder.AddDeveloperSigningCredential();
            RsaSecurityKey key = rsa.GetKey();
            builder.AddValidationKey(key);
            builder.AddSigningCredential(key, SecurityAlgorithms.RsaSha512);

            services.AddAuthentication()
                .AddMicrosoftAccount("Microsoft", microsoftOptions =>
                {
                    microsoftOptions.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    microsoftOptions.ClientId = appSettings.Authentication.Microsoft.ClientId;
                    microsoftOptions.ClientSecret = appSettings.Authentication.Microsoft.ClientSecret;
                })
                .AddGoogle("Google", googleOptions =>
                {
                    googleOptions.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    googleOptions.ClientId = appSettings.Authentication.Google.ClientId;
                    googleOptions.ClientSecret = appSettings.Authentication.Google.ClientSecret;
                })
                .AddGitHub("GitHub", githubOptions =>
                {
                    githubOptions.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    githubOptions.ClientId = appSettings.Authentication.GitHub.ClientId;
                    githubOptions.ClientSecret = appSettings.Authentication.GitHub.ClientSecret;
                })
                .AddTwitter("Twitter", twitterOptions =>
                {
                    twitterOptions.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    twitterOptions.ConsumerKey = appSettings.Authentication.Twitter.ConsumerAPIKey;
                    twitterOptions.ConsumerSecret = appSettings.Authentication.Twitter.ConsumerSecret;
                    twitterOptions.RetrieveUserDetails = true;
                })
                .AddFacebook("Facebook", facebookOptions =>
                {
                    facebookOptions.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    facebookOptions.AppId = appSettings.Authentication.Facebook.AppId;
                    facebookOptions.AppSecret = appSettings.Authentication.Facebook.AppSecret;
                });

            // Override the CookieAuthenticationOptions for DefaultCookieAuthenticationScheme
            // https://github.com/IdentityServer/IdentityServer4/blob/c30de032ec1dedc3b17dfa342043850638e84b43/src/IdentityServer4/src/Configuration/DependencyInjection/ConfigureInternalCookieOptions.cs#L28
            services.Configure<CookieAuthenticationOptions>(IdentityServerConstants.DefaultCookieAuthenticationScheme, options =>
            {
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.IsEssential = true;
            });

            if (_webHostEnvironment.EnvironmentName.Equals("Production"))
            {
                services.AddHsts(options =>
                {
                    options.Preload = true;
                    options.IncludeSubDomains = true;
                    options.MaxAge = TimeSpan.FromDays(730);
                });

                services.AddHttpsRedirection(options =>
                {
                    options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
                    options.HttpsPort = 443;
                });
            }
        }

        /// <summary>
        /// This required method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="env">IWebHostEnvironment</param>
        /// <param name="logger">ILogger&lt;Startup&gt;</param>
        /// <param name="lifetime">IHostApplicationLifetime</param>
        /// <param name="dbContext">AspIdUsersDbContext</param>
        /// <method>Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger&lt;Startup&gt; logger, IHostApplicationLifetime lifetime, AspIdUsersDbContext dbContext)</method>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger, IHostApplicationLifetime lifetime, AspIdUsersDbContext dbContext)
        {
            _logger = new Logger(logger);
            _logger.Trace($"Configure(IApplicationBuilder: {app}, IWebHostEnvironment: {env}, ILogger<Startup> {logger}, IHostApplicationLifetime: {lifetime})");

            AESGCM.Seed(_appSettings.SecretKey);
            DbInitializer.Initialize(dbContext);

            lifetime.ApplicationStarted.Register(OnAppStarted);
            lifetime.ApplicationStopping.Register(OnAppStopping);
            lifetime.ApplicationStopped.Register(OnAppStopped);

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseExceptionHandler("/Home/Error/500");
            app.UseStatusCodePagesWithRedirects("~/Home/Error/{0}");


            if (env.EnvironmentName.Equals("Production"))
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

        /// <summary>
        /// Exposed IApplicationLifetime interface method.
        /// </summary>
        /// <method>OnAppStarted()</method>
        public void OnAppStarted()
        {
            _logger.Information($"{Assembly.GetEntryAssembly().GetName().Name} Application Started");
            _logger.Information($"Hosting Environment: {_webHostEnvironment.EnvironmentName}");
        }

        /// <summary>
        /// Exposed IApplicationLifetime interface method.
        /// </summary>
        /// <method>OnAppStopping()</method>
        public void OnAppStopping()
        {
            _logger.Information($"{Assembly.GetEntryAssembly().GetName().Name} Application Shutdown");
        }

        /// <summary>
        /// Exposed IApplicationLifetime interface method.
        /// </summary>
        /// <method>OnAppStopped()</method>
        public void OnAppStopped()
        {
            _logger.Information($"{Assembly.GetEntryAssembly().GetName().Name} Application Ended");
        }
    }
}
