using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// Extension methods for existing HttpContext types.
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 09/29/2020 | Initial build |~ 
    /// </revision>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Method to return Absolute Uri for HttpContext
        /// </summary>
        /// <param name="httpContext">this HttpContext</param>
        /// <returns>Uri</returns>
        /// <method>GetAbsoluteUri(this HttpContext httpContext)</method>
        private static Uri GetAbsoluteUri(this HttpContext httpContext)
        {
            var request = httpContext.Request;
            UriBuilder uriBuilder = new UriBuilder()
            {
                Scheme = request.Scheme,
                Host = request.Host.Host
            };
            if (request.Host.Port != null)
            {
                uriBuilder.Port = (int)request.Host.Port;
            }

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Method to return Absolute Domain for HttpContext
        /// </summary>
        /// <param name="httpContext">this HttpContext</param>
        /// <returns>string</returns>
        /// <method>GetAbsoluteDomain(this HttpContext httpContext)</method>
        public static string GetAbsoluteDomain(this HttpContext httpContext)
        {
            return GetAbsoluteUri(httpContext).ToString();
        }

        /// <summary>
        /// Similar methods for Url/AbsolutePath which internally call GetAbsoluteUri
        /// </summary>
        /// <param name="httpContext">this HttpContext</param>
        /// <returns>string</returns>
        /// <method>GetAbsoluteUrl(this HttpContext httpContext)</method>
        public static string GetAbsoluteUrl(this HttpContext httpContext)
        {
            var uri = GetAbsoluteUri(httpContext);
            var request = httpContext.Request;
            UriBuilder uriBuilder = new UriBuilder(uri)
            {
                Path = request.Path.ToString(),
                Query = request.QueryString.ToString()
            };
            return uriBuilder.Uri.ToString();
        }

        /// <summary>
        /// Method to return Absolute Path HttpContext
        /// </summary>
        /// <param name="httpContext">this HttpContext</param>
        /// <returns>string</returns>
        /// <method>GetAbsolutePath(this HttpContext httpContext)</method>
        public static string GetAbsolutePath(this HttpContext httpContext)
        {
            return GetAbsoluteUri(httpContext).AbsolutePath;
        }

        /// <summary>
        /// Method to return if HttpContext is an AJax request
        /// </summary>
        /// <param name="httpContext">this HttpContext</param>
        /// <returns>bool</returns>
        /// <method>IsAjaxRequest(this HttpContext httpContext)</method>
        public static bool IsAjaxRequest(this HttpContext httpContext)
        {
            var request = httpContext.Request;
            return string.Equals(request.Query["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal) ||
                string.Equals(request.Headers["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal);
        }

        /// <summary>
        /// Method to return IPAddress of reomote address for HttpContext
        /// </summary>
        /// <param name="httpContext">HttpContext</param>
        /// <returns>IPAddress</returns>
        /// <method>GetRemoteAddress(this HttpContext httpContext)</method>
        public static IPAddress GetRemoteAddress(this HttpContext httpContext)
        {
            IPAddress ipAddress = httpContext.Connection.RemoteIpAddress;
            KeyValuePair<string, StringValues> xForwardedForHeader = httpContext.Request.Headers
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
    }
}
