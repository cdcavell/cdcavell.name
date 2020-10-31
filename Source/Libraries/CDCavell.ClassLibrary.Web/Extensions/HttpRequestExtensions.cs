using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// Extension methods for existing HttpRequest types.
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 09/29/2020 | Initial build |~ 
    /// </revision>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Method to determine if string is a local url
        /// </summary>
        /// <param name="request">this HttpRequest</param>
        /// <param name="url">string</param>
        /// <returns>bool</returns>
        /// <method>IsLocalUrl(this HttpRequest request, string url)</method>
        public static bool IsLocalUrl(this HttpRequest request, string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return false;
            }

            Uri absoluteUri;
            if (Uri.TryCreate(url, UriKind.Absolute, out absoluteUri))
            {
                return String.Equals(request.GetUri().Host, absoluteUri.Host,
                            StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                bool isLocal = !url.StartsWith("http:", StringComparison.OrdinalIgnoreCase)
                    && !url.StartsWith("https:", StringComparison.OrdinalIgnoreCase)
                    && Uri.IsWellFormedUriString(url, UriKind.Relative);
                return isLocal;
            }
        }

        /// <summary>
        /// Method to return Uri of HttpRequest
        /// </summary>
        /// <param name="request">this HttpRequest</param>
        /// <returns>bool</returns>
        /// <method>GetUri(this HttpRequest request)</method>
        public static Uri GetUri(this HttpRequest request)
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = request.Host.Host,
                Port = request.Host.Port.GetValueOrDefault(80),
                Path = request.Path.ToString(),
                Query = request.QueryString.ToString()
            };
            return uriBuilder.Uri;
        }

        /// <summary>
        /// Method to return IPAddress of reomote address for HttpRequest
        /// </summary>
        /// <param name="request">HttpRequest</param>
        /// <returns>IPAddress</returns>
        /// <method>GetRemoteAddress(this IPAddress ipAddress)</method>
        public static IPAddress GetRemoteAddress(this HttpRequest request)
        {
            IPAddress ipAddress = request.HttpContext.Connection.RemoteIpAddress;
            KeyValuePair<string, StringValues> xForwardedForHeader = request.HttpContext.Request.Headers
                .Where(x => x.Key.ToLower() == "x-forwarded-for")
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(xForwardedForHeader.Key))
            {
                if (!string.IsNullOrEmpty(xForwardedForHeader.Value))
                {
                    UriHostNameType uriType = Uri.CheckHostName(xForwardedForHeader.Value);
                    switch (uriType)
                    {
                        case UriHostNameType.IPv4:
                            // strip any port from xForwardedForHeader IP Address
                            string[] hostParts = xForwardedForHeader.Value.ToString().Split(':');
                            ipAddress = IPAddress.Parse(hostParts[0]);
                            break;
                        case UriHostNameType.IPv6:
                            ipAddress = IPAddress.Parse(xForwardedForHeader.Value);
                            break;
                    }
                }
            }

            return ipAddress;
        }

        /// <summary>
        /// Method to return if HttpRequest is an AJax request
        /// </summary>
        /// <param name="httpRequest">this HttpRequest</param>
        /// <returns>bool</returns>
        /// <method>IsAjaxRequest(this HttpRequest httpRequest)</method>
        public static bool IsAjaxRequest(this HttpRequest httpRequest)
        {
            return string.Equals(httpRequest.Query["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal) ||
                string.Equals(httpRequest.Headers["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal);
        }
    }
}
