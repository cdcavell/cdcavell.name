// Copyright (c) Brock Allen &amp; Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using CDCavell.ClassLibrary.Web.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace cdcavell.Filters
{
    /// <summary>
    /// Security Headers Attribute Filter from Brock Allen &amp; Dominick Baier.
    /// &lt;br /&gt;&lt;br /&gt;
    /// Copyright (c) Brock Allen &amp; Dominick Baier. All rights reserved.
    /// Licensed under the Apache License, Version 2.0. 
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 10/25/2020 | Initial build |~ 
    /// | Christopher D. Cavell | 1.0.0.3 | 10/28/2020 | Add YouTubeVideos |~ 
    /// | Christopher D. Cavell | 1.0.0.3 | 10/29/2020 | Remove YouTubeVideos (Not Implemented) |~ 
    /// | Christopher D. Cavell | 1.0.0.3 | 10/30/2020 | Issue #150 Content-Security-Policy HTTP header: Bad content security policy |~ 
    /// </revision>
    public class SecurityHeadersAttribute : ActionFilterAttribute
    {
        private string _StyleNonce;
        private string _ScriptNonce;

        /// <summary>
        /// Executes before result execution
        /// &lt;br /&gt;&lt;br /&gt;
        /// CSP Evaluator: https://csp-evaluator.appspot.com/
        /// </summary>
        /// <param name="context">ResultExecutingContext</param>
        /// <method>OnResultExecuting(ResultExecutingContext context)</method>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var result = context.Result;
            if (result is ViewResult)
            {
                // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Content-Type-Options
                if (!context.HttpContext.Response.Headers.ContainsKey("X-Content-Type-Options"))
                {
                    context.HttpContext.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                }

                // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Frame-Options
                if (!context.HttpContext.Response.Headers.ContainsKey("X-Frame-Options"))
                {
                    context.HttpContext.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                }

                // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy
                var csp = "default-src 'self'; ";
                csp += "img-src 'self' https://*.mm.bing.net data:; ";
                csp += "object-src 'none'; ";
                csp += "frame-ancestors 'self'; ";
                csp += "sandbox allow-modals allow-forms allow-same-origin allow-scripts allow-popups; ";
                csp += "base-uri 'self'; ";
                csp += "style-src 'self' 'nonce-" + _StyleNonce + "'; ";
                csp += "script-src 'strict-dynamic' 'nonce-" + _ScriptNonce + "'; ";
                // also consider adding upgrade-insecure-requests once you have HTTPS in place for production
                csp += "upgrade-insecure-requests; ";

                // once for standards compliant browsers
                if (!context.HttpContext.Response.Headers.ContainsKey("Content-Security-Policy"))
                {
                    context.HttpContext.Response.Headers.Add("Content-Security-Policy", csp);
                }
                // and once again for IE
                if (!context.HttpContext.Response.Headers.ContainsKey("X-Content-Security-Policy"))
                {
                    context.HttpContext.Response.Headers.Add("X-Content-Security-Policy", csp);
                }

                // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Referrer-Policy
                var referrer_policy = "no-referrer";
                if (!context.HttpContext.Response.Headers.ContainsKey("Referrer-Policy"))
                {
                    context.HttpContext.Response.Headers.Add("Referrer-Policy", referrer_policy);
                }

                // Additional security headers //
                // https://blog.elmah.io/the-asp-net-core-security-headers-guide/

                // The X-Xss-Protection header will cause most modern browsers to stop loading the page when a cross-site scripting attack is identified. 
                if (!context.HttpContext.Response.Headers.ContainsKey("X-Xss-Protection"))
                {
                    context.HttpContext.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
                }

                // Disable the possibility of Flash making cross-site requests. (Should not be using Flash, this is a safty catch)
                if (!context.HttpContext.Response.Headers.ContainsKey("X-Permitted-Cross-Domain-Policies"))
                {
                    context.HttpContext.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");
                }
            }
        }

        /// <summary>
        /// Executes after action method execution to set script nonce
        /// </summary>
        /// <param name="context">ActionExecutedContext</param>
        /// <method>OnActionExecuted(ActionExecutedContext context)</method>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = context.Controller as Controller;
            _StyleNonce = Nonce.Calculate();
            _ScriptNonce = Nonce.Calculate();
            controller.ViewBag.StyleNonce = _StyleNonce;
            controller.ViewBag.ScriptNonce = _ScriptNonce;
            base.OnActionExecuted(context);
        }
    }
}
