using CDCavell.ClassLibrary.Commons.Logging;
using CDCavell.ClassLibrary.Web.Mvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;

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
    public class ControllerActionPageFilter : ActionFilterAttribute
    {
        private readonly Logger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance
        /// </summary>
        /// <method>ControllerActionPageFilter(ILogger&lt;ControllerActionPageFilter&gt; logger, IHttpContextAccessor httpContextAccessor)</method>
        public ControllerActionPageFilter(ILogger<ControllerActionPageFilter> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = new Logger(logger);
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Executes before action method execution
        /// </summary>
        /// <param name="context">ActionExecutingContext</param>
        /// <method>OnActionExecuting(ActionExecutingContext context)</method>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            SetPageInformation(context);
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

        private void SetPageInformation(ActionExecutingContext context)
        {
            Controller controller = context.Controller as Controller;
            if (controller != null)
            {
                StringValues RequestId;
                StringValues ErrorMessage;
                object ActiveController;
                object ActiveAction;

                string returnUrl = context.HttpContext.Request.Headers["Referer"].ToString();

                context.HttpContext.Request.Query.TryGetValue("RequestId", out RequestId);
                context.HttpContext.Request.Query.TryGetValue("ErrorMessage", out ErrorMessage);
                context.RouteData.Values.TryGetValue("Controller", out ActiveController);
                context.RouteData.Values.TryGetValue("Action", out ActiveAction);

                if (string.IsNullOrEmpty(returnUrl))
                    returnUrl = _httpContextAccessor.HttpContext.Request.PathBase;
                else
                {
                    Uri uri = new Uri(returnUrl);
                    returnUrl = uri.AbsolutePath;
                }

                PageViewModel pageViewModel = new PageViewModel();
                pageViewModel.ReturnURL = returnUrl;
                pageViewModel.RequestId = RequestId;
                pageViewModel.ErrorMessage = ErrorMessage;
                pageViewModel.Controller = ActiveController.ToString();
                pageViewModel.Action = ActiveAction.ToString();

                controller.ViewData["PageViewModel"] = pageViewModel;
            }
        }
    }
}
