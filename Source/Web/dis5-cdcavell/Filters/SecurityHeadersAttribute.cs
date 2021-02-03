// Copyright (c) Duende Software. All rights reserved.
// See https://duendesoftware.com/license/identityserver.pdf for license information.

using CDCavell.ClassLibrary.Web.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace dis5_cdcavell.Filters
{
    /// <summary>
    /// Security Headers Attribute Filter 
    /// &lt;br /&gt;&lt;br /&gt;
    /// Copyright (c) Duende Software. All rights reserved.
    /// See https://duendesoftware.com/license/identityserver.pdf for license information. 
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.2.0 | 01/16/2021 | Initial build |~ 
    /// | Christopher D. Cavell | 1.0.3.0 | 02/02/2021 | Initial build Authorization Service |~ 
    /// </revision>
    public class SecurityHeadersAttribute : ActionFilterAttribute
    {
        private string _StyleNonce;
        private string _ScriptNonce;

        /// <summary>
        /// Executes before result execution
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
                csp += "img-src 'self' data:; ";
                csp += "object-src 'none'; ";
                csp += "frame-ancestors 'self'; ";
                csp += "frame-src 'self' https://as-ui-cdcavell.azurewebsites.net https://cdcavell.name; ";
                csp += "sandbox allow-forms allow-same-origin allow-scripts allow-popups; ";
                csp += "base-uri 'self'; ";
                csp += "style-src 'self' 'nonce-" + _StyleNonce + "'; ";
                csp += "script-src 'strict-dynamic' https: 'self' 'nonce-" + _ScriptNonce + "'; ";
                // also consider adding upgrade-insecure-requests once you have HTTPS in place for production
                csp += "upgrade-insecure-requests;";
                // also an example if you need client images to be displayed from twitter
                // csp += "img-src 'self' https://pbs.twimg.com;";

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
