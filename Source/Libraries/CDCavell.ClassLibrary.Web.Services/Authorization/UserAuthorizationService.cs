using CDCavell.ClassLibrary.Commons.Logging;
using CDCavell.ClassLibrary.Web.Http;
using CDCavell.ClassLibrary.Web.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
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
    /// | Christopher D. Cavell | 1.0.3.1 | 02/08/2021 | User Authorization Web Service |~ 
    /// </revision>
    public class UserAuthorizationService : IUserAuthorizationService
    {
        private Logger _logger;
        private UserAuthorizationModel _userAuthorization;
        private string _guid;
        private string _accessToken;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _authorizationServiceAPI;

        /// <value>string</value>
        public string Guid
        {
            get { return _guid; }
        }

        /// <value>string</value>
        public string AccessToken
        {
            get { return _accessToken; }
        }

        /// <value>List&lt;Claim&gt;</value>
        public List<Claim> AdditionalClaims
        {
            get
            {
                List<Claim> additionalClaims = new List<Claim>();
                additionalClaims.Add(new Claim("email", _userAuthorization.Email));
                additionalClaims.Add(new Claim("authorization", _guid));

                return additionalClaims;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">ILogger&lt;UserAuthorizationService&gt;</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        /// <param name="options">IOptions&lt;UserAuthorizationServiceOptionss&gt;</param>
        /// <method>UserAuthorizationService(ILogger&lt;UserAuthorizationService&gt; logger, IHttpContextAccessor httpContextAccessor, IOptions&lt;UserAuthorizationServiceOptions&gt; options)</method>
        public UserAuthorizationService(ILogger<UserAuthorizationService> logger, IHttpContextAccessor httpContextAccessor, IOptions<UserAuthorizationServiceOptions> options)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = new Logger(logger);
            _authorizationServiceAPI = options.Value.AuthorizationServiceAPI;
            _userAuthorization = new UserAuthorizationModel();
            _guid = System.Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Returns user authorization
        /// </summary>
        /// <param name="ticketReceivedContext">TicketReceivedContext</param>
        /// <returns>Task&lt;UserAuthorizationModel&gt;</returns>
        /// <method>InitialAuthorization(TicketReceivedContext ticketReceivedContext)</method>
        public async Task<UserAuthorizationModel> InitialAuthorization(TicketReceivedContext ticketReceivedContext)
        {
            // Get Access Token
            string accessToken = ticketReceivedContext.Properties.Items[".Token.access_token"];
            if (string.IsNullOrEmpty(accessToken))
                ThrowException("Invalid Access Token - Remote IP: " + ticketReceivedContext.HttpContext.GetRemoteAddress());

            _accessToken = accessToken;

            // Authorization Service API Get User Authorization
            JsonClient jsonClient = new JsonClient(_authorizationServiceAPI, accessToken);
            HttpStatusCode statusCode = await jsonClient.SendRequest(HttpMethod.Get, "Authorization");
            if (!jsonClient.IsResponseSuccess)
                ThrowException(jsonClient.GetResponseString() + " - Remote IP: " + ticketReceivedContext.HttpContext.GetRemoteAddress());

            string jsonString = AESGCM.Decrypt(jsonClient.GetResponseObject<string>(), accessToken);
            _userAuthorization = JsonConvert.DeserializeObject<UserAuthorizationModel>(jsonString);
            if (string.IsNullOrEmpty(_userAuthorization.Email))
                ThrowException("Email is null or empty - Remote IP: " + ticketReceivedContext.HttpContext.GetRemoteAddress());

            return _userAuthorization;
        }

        private void ThrowException(string message)
        {
            Exception exception = new Exception(message);
            _logger.Exception(exception);
            throw exception;
        }
    }
}
