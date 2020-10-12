using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

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
    /// | Christopher D. Cavell | 1.0.0 | 10/12/2020 | Initial build |~ 
    /// </revision>
    public class JsonClient
    {
        private string _baseUrl;
        private string _returnMessage;
        private List<KeyValuePair<string, string>> _headers;

        private HttpStatusCode _statusCode = HttpStatusCode.NoContent;

        private bool _responseSuccess = false;

        /// <value>HttpStatusCode</value>
        public HttpStatusCode StatusCode { get { return _statusCode; } }

        /// <value>bool</value>
        public bool IsResponseSuccess { get { return _responseSuccess; } }

        /// <value>TimeSpan</value>
        public TimeSpan TimeOut { get; set; }

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="baseUrl">string</param>
        /// <method>JsonClient(string baseUrl)</method>
        public JsonClient(string baseUrl)
        {
            if (string.IsNullOrEmpty(baseUrl))
                throw new InvalidOperationException("Invalid baseUrl");

            if (baseUrl[baseUrl.Length - 1] != '/' && baseUrl[baseUrl.Length - 1] != '\\')
                baseUrl += "/";

            _baseUrl = baseUrl;

            _headers = new List<KeyValuePair<string, string>>();
        }

        /// <summary>
        /// Send request ignoring self signed certificate errors
        /// </summary>
        /// <param name="httpMethod">HttpMethod</param>
        /// <param name="requestUri">string</param>
        /// <param name="content">object</param>
        /// <returns>HttpStatusCode</returns>
        /// <method>SendRequest(HttpMethod httpMethod, string requestUri, object content)</method>
        public HttpStatusCode SendRequest(HttpMethod httpMethod, string requestUri, object content)
        {
            return SendRequest(httpMethod, requestUri, content, true);
        }

        /// <summary>
        /// Send request
        /// </summary>
        /// <param name="httpMethod">HttpMethod</param>
        /// <param name="requestUri">string</param>
        /// <param name="content">object</param>
        /// <param name="ignoreSelfSignedError">bool</param>
        /// <returns>HttpStatusCode</returns>
        /// <method>SendRequest(HttpMethod httpMethod, string requestUri, object content, bool ignoreSelfSignedError)</method>
        public HttpStatusCode SendRequest(HttpMethod httpMethod, string requestUri, object content, bool ignoreSelfSignedError)
        {
            _statusCode = HttpStatusCode.BadRequest;
            using (HttpClientHandler clientHandler = new HttpClientHandler())
            {
                clientHandler.UseDefaultCredentials = true;
                if (ignoreSelfSignedError)
                    // This is to take care of (The SSL connection could not be established) errors
                    // with self signed certificates 
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                using (var client = new HttpClient(clientHandler))
                {
                    HttpRequestMessage request = new HttpRequestMessage(httpMethod, _baseUrl + requestUri);
                    request.Properties["RequestTimeout"] = (TimeOut == null) ? TimeSpan.FromMinutes(1) : TimeOut;
                    request.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

                    // Adding any additional headers for request here
                    if (_headers.Count > 0)
                        foreach (KeyValuePair<string, string> header in _headers)
                            request.Headers.Add(header.Key, header.Value);

                    HttpResponseMessage response = client.SendAsync(request).Result;
                    _statusCode = response.StatusCode;

                    if (response.IsSuccessStatusCode)
                    {
                        _returnMessage = response.Content.ReadAsStringAsync().Result;
                        _responseSuccess = true;
                    }
                    else
                        _returnMessage = Html.StatusCodes.ToString((int)response.StatusCode);

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
