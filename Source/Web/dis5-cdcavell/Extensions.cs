using dis5_cdcavell.Models.Account;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace dis5_cdcavell
{
    /// <summary>
    /// Extensions
    /// &lt;br /&gt;&lt;br /&gt;
    /// Copyright (c) Duende Software. All rights reserved.
    /// See https://duendesoftware.com/license/identityserver.pdf for license information. 
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.2.0 | 01/16/2021 | Initial build |~ 
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
