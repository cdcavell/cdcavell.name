using CDCavell.ClassLibrary.Web.Html;
using System;
using System.Collections.Generic;

namespace CDCavell.ClassLibrary.Web.Mvc.Models
{
    /// <summary>
    /// Error view model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 09/29/2020 | Initial build |~ 
    /// </revision>
    public class ErrorViewModel
    {
        /// <value>int</value>
        public int StatusCode { get; set; }
        /// <value>string</value>
        public string StatusMessage { get; set; }
        /// <value>Exception</value>
        public Exception Exception { get; set; }
        /// <value>string</value>
        public string RequestId { get; set; }

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="statusCode">int</param>
        /// <method>ErrorViewModel(int statusCode)</method>
        public ErrorViewModel(int statusCode)
        {
            KeyValuePair<int, string> kvp = StatusCodes.GetCodeDefinition(statusCode);
            StatusCode = kvp.Key;
            StatusMessage = kvp.Value;
        }
    }
}
