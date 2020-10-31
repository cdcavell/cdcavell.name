using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Net
{
    /// <summary>
    /// Extension methods for existing IPAddress types.
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 09/29/2020 | Initial build |~ 
    /// </revision>
    public static class IPAddressExtensions
    {
        /// <summary>
        /// Method to return IPAddress of reomote address of supplied HttpRequest
        /// </summary>
        /// <param name="ipAddress">this IPAddress</param>
        /// <param name="request">HttpRequest</param>
        /// <returns>IPAddress</returns>
        /// <method>GetRemoteAddress(this IPAddress ipAddress)</method>
        public static IPAddress GetRemoteAddress(this IPAddress ipAddress, HttpRequest request)
        {
            ipAddress = request.HttpContext.Connection.RemoteIpAddress;
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
        /// An extension method to determine if an IP address is internal, as specified in RFC1918
        /// </summary>
        /// <param name="ipAddress">The IP address that will be tested</param>
        /// <returns>Returns true if the IP is internal, false if it is external</returns>
        /// <method>IsInternalRequest(this IPAddress ipAddress)</method>
        public static bool IsInternalAddress(this IPAddress ipAddress)
        {
            // how to determine whether an IP address in private?
            // https://stackoverflow.com/questions/8113546/how-to-determine-whether-an-ip-address-in-private

            int[] ipAddressArray = ipAddress.MapToIPv4()
                .ToString().Split(new String[] { "." }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s)).ToArray();

            // loopback address
            if (ipAddressArray[0] == 0 &&
                ipAddressArray[1] == 0 &&
                ipAddressArray[2] == 0 &&
                ipAddressArray[3] == 1)
            {
                return true;
            }

            // in private ip range
            if (ipAddressArray[0] == 10 ||
                (ipAddressArray[0] == 192 && ipAddressArray[1] == 168) ||
                (ipAddressArray[0] == 172 && (ipAddressArray[1] >= 16 && ipAddressArray[1] <= 31)))
            {
                return true;
            }

            return false;
        }
    }
}
