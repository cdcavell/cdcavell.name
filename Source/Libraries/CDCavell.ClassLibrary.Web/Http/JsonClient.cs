using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CDCavell.ClassLibrary.Web.Http
{
    /// <summary>
    /// Http client to handle json requests. Each request defaults to one minute timeout 
    /// and can be overriden with TimeOut property.
    /// </summary>
    /// <example>
    /// JsonClient client = new JsonClient("https://SomeAPI.com");
    /// client.TimeOut = TimeSpan.FromMinutes(2);
    /// client.AddRequestHeader("MyHeader", "Some Custome Header String");
    /// 
    /// HttpStatusCode statusCode = client.SendRequest(HttpMethod.Post, "APIService", "Request Content");
    /// if (client.IsResponseSuccess)
    /// {
    ///     string response = client.GetResponseString();
    ///     // or    
    ///     MyObject myObject = client.GetResponseObject&lt;MyObject&gt;();    
    /// }
    /// </example>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 10/12/2020 | Initial build |~ 
    /// | Christopher D. Cavell | 1.0.0.9 | 11/08/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// | Christopher D. Cavell | 1.0.1.0 | 11/25/2020 | Update: Target Framework netcoreapp3.1 to net5.0 |~ 
    /// | Christopher D. Cavell | 1.0.3.0 | 01/23/2021 | Initial build Authorization Service |~ 
    /// | Christopher D. Cavell | 1.0.3.1 | 02/07/2021 | User Authorization Web Service |~ 
    /// </revision>
    public class JsonClient
    {
        private readonly string _baseUrl;
        private string _returnMessage;
        private readonly List<KeyValuePair<string, string>> _headers;

        private HttpStatusCode _statusCode = HttpStatusCode.NoContent;

        private bool _responseSuccess = false;

        /// <value>HttpStatusCode</value>
        public HttpStatusCode StatusCode { get { return _statusCode; } }

        /// <value>bool</value>
        public bool IsResponseSuccess { get { return _responseSuccess; } }

        /// <value>TimeSpan</value>
        public TimeSpan TimeOut { get; set; } = TimeSpan.FromMinutes(1);

        /// <value>string</value>
        public string BearerToken { get; set; }

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="baseUrl">string</param>
        /// <method>JsonClient(string baseUrl)</method>
        public JsonClient(string baseUrl)
        {
            if (string.IsNullOrEmpty(baseUrl))
                throw new InvalidOperationException("Invalid baseUrl");

            if (baseUrl[^1] != '/' && baseUrl[^1] != '\\')
                    baseUrl += "/";

            _baseUrl = baseUrl;

            _headers = new List<KeyValuePair<string, string>>();
        }


        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="baseUrl">string</param>
        /// <param name="bearerToken">string</param>
        /// <method>JsonClient(string baseUrl)</method>
        public JsonClient(string baseUrl, string bearerToken) : this(baseUrl)
        {
            this.BearerToken = bearerToken;
        }

        /// <summary>
        /// Send request ignoring self signed certificate errors
        /// </summary>
        /// <param name="httpMethod">HttpMethod</param>
        /// <param name="requestUri">string</param>
        /// <returns>Task&lt;HttpStatusCode&gt;</returns>
        /// <method>SendRequest(HttpMethod httpMethod, string requestUri)</method>
        public async Task<HttpStatusCode> SendRequest(HttpMethod httpMethod, string requestUri)
        {
            return await SendRequest(httpMethod, requestUri, null);
        }

        /// <summary>
        /// Send request ignoring self signed certificate errors
        /// </summary>
        /// <param name="httpMethod">HttpMethod</param>
        /// <param name="requestUri">string</param>
        /// <param name="content">object</param>
        /// <returns>Task&lt;HttpStatusCode&gt;</returns>
        /// <method>SendRequest(HttpMethod httpMethod, string requestUri, object content)</method>
        public async Task<HttpStatusCode> SendRequest(HttpMethod httpMethod, string requestUri, object content)
        {
            return await SendRequest(httpMethod, requestUri, content, true);
        }

        /// <summary>
        /// Send request
        /// </summary>
        /// <param name="httpMethod">HttpMethod</param>
        /// <param name="requestUri">string</param>
        /// <param name="content">object</param>
        /// <param name="ignoreSelfSignedError">bool</param>
        /// <returns>Task&lt;HttpStatusCode&gt;</returns>
        /// <method>SendRequest(HttpMethod httpMethod, string requestUri, object content, bool ignoreSelfSignedError)</method>
        public async Task<HttpStatusCode> SendRequest(HttpMethod httpMethod, string requestUri, object content, bool ignoreSelfSignedError)
        {
            _statusCode = HttpStatusCode.BadRequest;
            using (HttpClientHandler clientHandler = new HttpClientHandler())
            {
                clientHandler.UseDefaultCredentials = true;
                if (ignoreSelfSignedError)
                    // This is to take care of (The SSL connection could not be established) errors
                    // with self signed certificates 
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                using var client = new HttpClient(clientHandler);
                {
                    // Adding any bearer token for request here
                    if (!string.IsNullOrEmpty(BearerToken))
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);

                    HttpRequestMessage request = new HttpRequestMessage(httpMethod, _baseUrl + requestUri);
                    request.Options.Set(new HttpRequestOptionsKey<TimeSpan>("RequestTimeout"), TimeOut);
                    request.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

                    // Adding any additional headers for request here
                    if (_headers.Count > 0)
                        foreach (KeyValuePair<string, string> header in _headers)
                            request.Headers.Add(header.Key, header.Value);

                    try
                    {
                        HttpResponseMessage response = await client.SendAsync(request);
                        _statusCode = response.StatusCode;

                        if (response.IsSuccessStatusCode)
                        {
                            _returnMessage = await response.Content.ReadAsStringAsync();
                            _responseSuccess = true;
                        }
                        else
                            _returnMessage = Html.StatusCodes.ToString((int)response.StatusCode);
                    }
                    catch (Exception exception)
                    {
                        _statusCode = HttpStatusCode.InternalServerError;
                        _returnMessage = exception.Message;
                        if (exception.InnerException != null)
                            if (!string.IsNullOrEmpty(exception.InnerException.Message))
                                _returnMessage = exception.InnerException.Message;
                    }

                    // clear any additional request headers that may have been set
                    _headers.Clear();
                }
            }
            return _statusCode;
        }

        /// <summary>
        /// Get response string
        /// </summary>
        /// <returns>string</returns>
        /// <method>GetResponseString()</method>
        public string GetResponseString()
        {
            if (!string.IsNullOrEmpty(_returnMessage))
                return _returnMessage;

            return string.Empty;
        }

        /// <summary>
        /// Get response object
        /// </summary>
        /// <returns>T</returns>
        /// <method>GetResponseObject&lt;T&gt;()</method>
        public T GetResponseObject<T>()
        {
            var result = JsonConvert.DeserializeObject<T>(_returnMessage);
            return result;
        }

        /// <summary>
        /// Add request header
        /// </summary>
        /// <param name="name">string</param>
        /// <param name="value">value</param>
        /// <method>AddRequestHeader(string name, string value)</method>
        public void AddRequestHeader(string name, string value)
        {
            _headers.Add(new KeyValuePair<string, string>(name, value));
        }
    }
}
