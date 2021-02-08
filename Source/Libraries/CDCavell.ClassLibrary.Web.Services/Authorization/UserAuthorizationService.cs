using CDCavell.ClassLibrary.Commons.Logging;
using CDCavell.ClassLibrary.Web.Http;
using CDCavell.ClassLibrary.Web.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CDCavell.ClassLibrary.Web.Services.Authorization
{
    /// <summary>
    /// User Authorization Web Service
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.1 | 02/07/2021 | User Authorization Web Service |~ 
    /// </revision>
    public class UserAuthorizationService : IUserAuthorizationService
    {
        private Logger _logger;
        private readonly HttpContext _httpContext;
        private readonly string _authorizationServiceAPI;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpContext">HttpContext</param>
        /// <param name="logger">ILogger&lt;UserAuthorizationService&gt;</param>
        /// <param name="options">IOptions&lt;UserAuthorizationServiceOptionss&gt;</param>
        /// <method>UserAuthorizationService(ILogger&lt;UserAuthorizationService&gt; logger, HttpContext httpContext, IOptions&lt;UserAuthorizationServiceOptionss&gt; options)</method>
        public UserAuthorizationService(ILogger<UserAuthorizationService> logger, HttpContext httpContext, IOptions<UserAuthorizationServiceOptions> options)
        {
            _httpContext = httpContext;
            _logger = new Logger(logger);
            _authorizationServiceAPI = options.Value.AuthorizationServiceAPI;
        }

        public async Task<UserAuthorizationModel> InitialAuthorization(TicketReceivedContext ticketReceivedContext)
        {
            // Get Access Token
            string accessToken = ticketReceivedContext.Properties.Items[".Token.access_token"];
            if (string.IsNullOrEmpty(accessToken))
                ThrowException("Invalid Access Token - Remote IP: " + ticketReceivedContext.HttpContext.GetRemoteAddress());

            // Authorization Service API Get User Authorization
            JsonClient jsonClient = new JsonClient(_authorizationServiceAPI, accessToken);
            HttpStatusCode statusCode = await jsonClient.SendRequest(HttpMethod.Get, "Authorization");
            if (!jsonClient.IsResponseSuccess)
                ThrowException(jsonClient.GetResponseString() + " - Remote IP: " + ticketReceivedContext.HttpContext.GetRemoteAddress());

            string jsonString = AESGCM.Decrypt(jsonClient.GetResponseObject<string>(), accessToken);
            UserAuthorizationModel userAuthorization = JsonConvert.DeserializeObject<UserAuthorizationModel>(jsonString);
            if (string.IsNullOrEmpty(userAuthorization.Email))
                ThrowException("Email is null or empty - Remote IP: " + ticketReceivedContext.HttpContext.GetRemoteAddress());

            return userAuthorization;
        }

        private void ThrowException(string message)
        {
            Exception exception = new Exception(message);
            _logger.Exception(exception);
            throw exception;
        }
    }
}
