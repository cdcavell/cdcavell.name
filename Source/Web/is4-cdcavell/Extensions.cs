using IdentityServer4.Models;
using is4_cdcavell.Models.Account;
using Microsoft.AspNetCore.Mvc;
using System;

namespace is4_cdcavell
{
    /// <summary>
    /// Extensions
    /// &lt;br /&gt;&lt;br /&gt;
    /// Copyright (c) Brock Allen &amp; Dominick Baier. All rights reserved.
    /// Licensed under the Apache License, Version 2.0. 
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 09/30/2020 | Initial build |~ 
    /// </revision>
    public static class Extensions
    {
        /// <summary>
        /// Checks if the redirect URI is for a native client.
        /// </summary>
        /// <returns>bool</returns>
        /// <method>IsNativeClient(this AuthorizationRequest context)</method>
        public static bool IsNativeClient(this AuthorizationRequest context)
        {
            return !context.RedirectUri.StartsWith("https", StringComparison.Ordinal)
               && !context.RedirectUri.StartsWith("http", StringComparison.Ordinal);
        }

        /// <summary>
        /// Loading page.
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <method>LoadingPage(this Controller controller, string viewName, string redirectUri)</method>
        public static IActionResult LoadingPage(this Controller controller, string viewName, string redirectUri)
        {
            controller.HttpContext.Response.StatusCode = 200;
            controller.HttpContext.Response.Headers["Location"] = "";

            return controller.View(viewName, new RedirectViewModel { RedirectUrl = redirectUri });
        }
    }
}
