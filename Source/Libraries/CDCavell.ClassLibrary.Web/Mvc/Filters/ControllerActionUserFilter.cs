using CDCavell.ClassLibrary.Commons.Logging;
using CDCavell.ClassLibrary.Web.Mvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace CDCavell.ClassLibrary.Web.Mvc.Filters
{
    /// <summary>
    /// Action user filter that runs before and after action method execution
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 10/12/2020 | Initial build |~ 
    /// </revision>
    [AttributeUsage(AttributeTargets.Class)]
    public class ControllerActionUserFilter : ActionFilterAttribute
    {
        private readonly Logger _logger;

        /// <summary>
        /// Initializes a new instance
        /// </summary>
        /// <method>ControllerActionUserFilter()</method>
        public ControllerActionUserFilter(ILogger<ControllerActionUserFilter> logger)
        {
            _logger = new Logger(logger);
        }

        /// <summary>
        /// Executes before action method execution
        /// </summary>
        /// <param name="context">ActionExecutingContext</param>
        /// <method>OnActionExecuting(ActionExecutingContext context)</method>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            SetUserInformation(context);
            base.OnActionExecuting(context);
        }

        /// <summary>
        /// Executes after action method execution
        /// </summary>
        /// <param name="context">ActionExecutedContext</param>
        /// <method>OnActionExecuted(ActionExecutedContext context)</method>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

        private void SetUserInformation(ActionExecutingContext context)
        {
            Controller controller = context.Controller as Controller;
            if (controller != null)
            {
                var user = context.HttpContext.User;
                if (user != null)
                {
                    foreach (Claim claim in user.Claims)
                        _logger.Trace("*** TYPE: " + claim.Type + " Value: " + claim.Value);
                }

                Claim nameClaim = (user != null) ? user.FindFirst("user_name") ?? user.FindFirst("name") : null;
                Claim emailClaim = (user != null) ? user.FindFirst(ClaimTypes.Email) ?? user.FindFirst("email") : null;
                Claim userIdClaim = (user != null) ? user.FindFirst("user_id") : null;

                UserViewModel userViewModel = new UserViewModel();
                userViewModel.IsAuthenticated = (user != null) ? user.Identity.IsAuthenticated : false;
                userViewModel.Id = (userIdClaim != null) ? userIdClaim.Value : string.Empty;
                userViewModel.Name = (nameClaim != null) ? nameClaim.Value : (user != null) ? user.Identity.Name ?? string.Empty : string.Empty;
                userViewModel.Email = (emailClaim != null) ? emailClaim.Value : string.Empty;

                List<string> roles = new List<string>();
                if (user.Claims != null)
                    foreach (Claim claim in user.Claims.Where(x => x.Type == ClaimTypes.Role))
                        roles.Add(claim.Value);

                userViewModel.Roles = roles;
                userViewModel.IPAddress = context.HttpContext.GetRemoteAddress();

                controller.ViewData["UserViewModel"] = userViewModel;
            }
        }
    }
}
