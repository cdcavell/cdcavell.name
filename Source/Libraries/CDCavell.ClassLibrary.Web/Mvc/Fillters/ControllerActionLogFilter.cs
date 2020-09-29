﻿using CDCavell.ClassLibrary.Commons.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace CDCavell.ClassLibrary.Web.Mvc.Fillters
{
    /// <summary>
    /// Action logging filter that runs before and after action method execution
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 09/29/2020 | Initial build |~ 
    /// </revision>
    [AttributeUsage(AttributeTargets.Class)]
    public class ControllerActionLogFilter : ActionFilterAttribute
    {
        private readonly Logger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _userName;

        /// <summary>
        /// Initializes a new instance
        /// </summary>
        /// <method>ControllerActionLogFilter(ILogger&lt;ControllerActionLogFilter&gt; logger, IHttpContextAccessor httpContextAccessor)</method>
        public ControllerActionLogFilter(ILogger<ControllerActionLogFilter> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = new Logger(logger);
            _httpContextAccessor = httpContextAccessor;

            var user = _httpContextAccessor.HttpContext.User;
            Claim nameClaim = (user != null) ? user.FindFirst(ClaimTypes.Name) ?? user.FindFirst("name") : null;
            Claim userIdClaim = (user != null) ? user.FindFirst("user_id") : null;
            _userName = ((nameClaim != null) ? nameClaim.Value : "Anonymous") + " (" + ((userIdClaim != null) ? userIdClaim.Value : string.Empty) + ")";
        }

        /// <summary>
        /// Executes before action method execution
        /// </summary>
        /// <param name="context">ActionExecutingContext</param>
        /// <method>OnActionExecuting(ActionExecutingContext context)</method>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Log("OnActionExecuting:", context.RouteData, context.HttpContext);
            base.OnActionExecuting(context);
        }

        /// <summary>
        /// Executes after action method execution
        /// </summary>
        /// <param name="context">ActionExecutedContext</param>
        /// <method>OnActionExecuted(ActionExecutedContext context)</method>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Log("OnActionExecuted:", context.RouteData, context.HttpContext);
            base.OnActionExecuted(context);
        }

        private void Log(string message, RouteData routeData, HttpContext httpContext)
        {
            string controller = routeData.Values["controller"] != null ? routeData.Values["controller"].ToString() : string.Empty;
            string action = routeData.Values["action"] != null ? routeData.Values["action"].ToString() : string.Empty;
            string id = routeData.Values["id"] != null ? routeData.Values["id"].ToString() : string.Empty;

            ConnectionInfo connectionInfo = httpContext.Connection;
            string localIpAddress = connectionInfo.LocalIpAddress.MapToIPv4().ToString();
            string localPort = connectionInfo.LocalPort.ToString();
            string remoteIpAddress = httpContext.GetRemoteAddress().MapToIPv4().ToString();
            string remotePort = connectionInfo.RemotePort.ToString();

            StringBuilder sb = new StringBuilder(message.Trim());
            sb.Append(" ");
            sb.Append(controller);
            sb.Append("/");
            sb.Append(action);

            if (!string.IsNullOrEmpty(id))
            {
                sb.Append("/");
                sb.Append(id);
            }

            sb.Append(" Local: Address[");
            sb.Append(localIpAddress);
            sb.Append("] Port [");
            sb.Append(localPort);
            sb.Append("] Remote: Address[");
            sb.Append(remoteIpAddress);
            sb.Append("] Port [");
            sb.Append(remotePort);
            sb.Append("] ");

            sb.Append(" User Name: ");
            sb.Append(_userName);

            _logger.Trace(sb.ToString());
        }
    }
}