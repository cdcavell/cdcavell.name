using AspNetCore.SEOHelper;
using cdcavell.Authorization;
using cdcavell.Classes;
using cdcavell.Data;
using cdcavell.Filters;
using cdcavell.Models.AppSettings;
using CDCavell.ClassLibrary.Commons.Logging;
using CDCavell.ClassLibrary.Web.Http;
using CDCavell.ClassLibrary.Web.Mvc.Filters;
using CDCavell.ClassLibrary.Web.Mvc.Models.Authorization;
using CDCavell.ClassLibrary.Web.Security;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace cdcavell
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
    /// | Christopher D. Cavell | 1.0.0.0 | 10/28/2020 | Initial build |~ 
    /// | Christopher D. Cavell | 1.0.0.4 | 10/30/2020 | Enforce HTTPS in ASP.NET Core [#158](https://github.com/cdcavell/cdcavell.name/issues/158) |~
    /// | Christopher D. Cavell | 1.0.0.5 | 10/31/2020 | EU General Data Protection Regulation (GDPR) support in ASP.NET Core [#161](https://github.com/cdcavell/cdcavell.name/issues/161) |~
    /// | Christopher D. Cavell | 1.0.0.7 | 10/31/2020 | Serve static assets with an efficient cache policy [#172](https://github.com/cdcavell/cdcavell.name/issues/172) |~
    /// | Christopher D. Cavell | 1.0.0.7 | 10/31/2020 | Integrate Bing’s Adaptive URL submission API with your website [#144](https://github.com/cdcavell/cdcavell.name/issues/144) |~ 
    /// | Christopher D. Cavell | 1.0.0.9 | 11/11/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// | Christopher D. Cavell | 1.0.2.2 | 01/18/2021 | Convert GrantType from Implicit to Pkce |~ 
    /// | Christopher D. Cavell | 1.0.3.0 | 10/24/2020 | Initial build Authorization Service |~ 
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
            services.AddSingleton(appSettings);

            // cache authority discovery and add to DI
            services.AddHttpClient();
            services.AddSingleton<IDiscoveryCache>(options =>
            {
                var factory = options.GetRequiredService<IHttpClientFactory>();
                return new DiscoveryCache(_appSettings.Authentication.IdP.Authority, () => factory.CreateClient());
            });

            services.AddDbContext<CDCavellDbContext>(options =>
                options.UseSqlite(appSettings.ConnectionStrings.CDCavellConnection));

            services.AddMvc();
            services.AddControllersWithViews();

            // Register IHttpContextAccessor
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Register controller fillters
            services.AddScoped<SecurityHeadersAttribute>();
            services.AddScoped<ControllerActionLogFilter>();
            services.AddScoped<ControllerActionUserFilter>();
            services.AddScoped<ControllerActionPageFilter>();


            // Register Application Authorization
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Authenticated", policy =>
                {
                    policy.Requirements.Add(new AuthenticatedRequirement(true));
                });
            });

            // Registered authorization handlers
            services.AddTransient<IAuthorizationHandler, AuthenticatedHandler>();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme; 
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, cookieOptions => {
                    cookieOptions.Events.OnRedirectToAccessDenied = context =>
                    {
                        context.Response.StatusCode = (int)(HttpStatusCode.Unauthorized);
                        return Task.CompletedTask;
                    };
                    cookieOptions.Events.OnSigningOut = context =>
                    {
                        context.CookieOptions.Expires = DateTime.Now.AddDays(-1);
                        return Task.CompletedTask;
                    };
                })
                .AddOpenIdConnect("oidc", options =>
                {
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; 

                    options.Authority = appSettings.Authentication.IdP.Authority;
                    options.RequireHttpsMetadata = true;

                    options.ClientId = appSettings.Authentication.IdP.ClientId;
                    options.ResponseType = OpenIdConnectResponseType.Code;
                    options.ResponseMode = "form_post";

                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("email");
                    options.SaveTokens = true;

                    options.Events = new OpenIdConnectEvents
                    {
                        OnRedirectToIdentityProvider = redirectContext =>
                        {
                            return Task.FromResult(0);
                        },

                        OnTicketReceived = ticketReceivedContext =>
                        {
                            // Get Access Token
                            string accessToken = ticketReceivedContext.Properties.Items[".Token.access_token"];
                            if (string.IsNullOrEmpty(accessToken))
                            {
                                _logger.Exception(new Exception("Invalid Access Token - Remote IP: " + ticketReceivedContext.HttpContext.GetRemoteAddress()));
                                ticketReceivedContext.HttpContext.Response.Redirect("/Home/Error/7001");
                                ticketReceivedContext.HandleResponse();
                                return Task.FromResult(ticketReceivedContext.Result);
                            }

                            // Authorization Service API Get User Authorization
                            JsonClient jsonClient = new JsonClient(_appSettings.Authorization.AuthorizationService.API, accessToken);
                            HttpStatusCode statusCode = jsonClient.SendRequest(HttpMethod.Get, "Authorization");
                            if (!jsonClient.IsResponseSuccess)
                            {
                                _logger.Exception(new Exception(jsonClient.GetResponseString() + " - Remote IP: " + ticketReceivedContext.HttpContext.GetRemoteAddress()));
                                ticketReceivedContext.HttpContext.Response.Redirect("/Home/Error/7002");
                                ticketReceivedContext.HandleResponse();
                                return Task.FromResult(ticketReceivedContext.Result);
                            }

                            string jsonString = AESGCM.Decrypt(jsonClient.GetResponseObject<string>(), accessToken);
                            UserAuthorization userAuthorization = JsonConvert.DeserializeObject<UserAuthorization>(jsonString);
                            if (string.IsNullOrEmpty(userAuthorization.Email))
                            {
                                _logger.Exception(new Exception("Email is null or empty - Remote IP: " + ticketReceivedContext.HttpContext.GetRemoteAddress()));
                                ticketReceivedContext.HttpContext.Response.Redirect("/Home/Error/7003");
                                ticketReceivedContext.HandleResponse();
                                return Task.FromResult(ticketReceivedContext.Result);
                            }

                            //TODO: If new UserAuthorization then redirect to new registration process

                            // Get dbContext
                            CDCavellDbContext dbContext = (CDCavellDbContext)ticketReceivedContext.HttpContext
                                .RequestServices.GetService(typeof(CDCavellDbContext));

                            // Harden User Authorization
                            Data.Authorization authorization = new Data.Authorization();
                            authorization.Guid = Guid.NewGuid().ToString();
                            authorization.Created = DateTime.Now;
                            authorization.UserAuthorization = userAuthorization;
                            authorization.AddUpdate(dbContext);

                            var additionalClaims = new List<Claim>();
                            if (!ticketReceivedContext.Principal.HasClaim("email", userAuthorization.Email.Clean()))
                                additionalClaims.Add(new Claim("email", userAuthorization.Email.Clean()));

                            if (!ticketReceivedContext.Principal.HasClaim("authorization", authorization.Guid))
                                additionalClaims.Add(new Claim("authorization", authorization.Guid));

                            ticketReceivedContext.Principal.AddIdentity(new ClaimsIdentity(additionalClaims));

                            return Task.FromResult(ticketReceivedContext.Result); 
                        }
                    };
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

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
                options.ConsentCookie.Expiration = TimeSpan.FromDays(30);
            });
        }

        /// <summary>
        /// This required method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="env">IWebHostEnvironment</param>
        /// <param name="logger">ILogger&lt;Startup&gt;</param>
        /// <param name="lifetime">IHostApplicationLifetime</param>
        /// <param name="dbContext">CDCavellDbContext</param>
        /// <method>Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger&lt;Startup&gt; logger, IHostApplicationLifetime lifetime, CDCavellDbContext dbContext)</method>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger, IHostApplicationLifetime lifetime, CDCavellDbContext dbContext)
        {
            _logger = new Logger(logger);
            _logger.Trace($"Configure(IApplicationBuilder: {app}, IWebHostEnvironment: {env}, ILogger<Startup> {logger}, IHostApplicationLifetime: {lifetime})");

            AESGCM.Seed(_configuration);
            DbInitializer.Initialize(dbContext);
            new Sitemap(_logger, _webHostEnvironment, _appSettings).Create(dbContext);

            lifetime.ApplicationStarted.Register(OnAppStarted);
            lifetime.ApplicationStopping.Register(OnAppStopping);
            lifetime.ApplicationStopped.Register(OnAppStopped);

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseExceptionHandler("/Home/Error/500");
            app.UseStatusCodePagesWithRedirects("~/Home/Error/{0}");
            app.UseXMLSitemap(env.ContentRootPath);

            if (env.EnvironmentName.Equals("Production"))
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles(new StaticFileOptions {
                OnPrepareResponse = ctx =>
                {
                    const int durationInSeconds = 60 * 60 * 24 * 365;
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] =
                        "public,max-age=" + durationInSeconds;
                }
            });

            app.UseCookiePolicy();
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
