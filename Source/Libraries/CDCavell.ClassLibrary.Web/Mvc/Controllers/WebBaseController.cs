using CDCavell.ClassLibrary.Commons.Logging;
using CDCavell.ClassLibrary.Web.Html;
using CDCavell.ClassLibrary.Web.Mvc.Filters;
using CDCavell.ClassLibrary.Web.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;

namespace CDCavell.ClassLibrary.Web.Mvc.Controllers
{
    /// <class>WebBaseController</class>
    /// <summary>
    /// Base controller class for web application
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 10/12/2020 | Initial build |~ 
    /// | Christopher D. Cavell | 1.0.2.1 | 01/17/2021 | Handle HttpRequestException as http status instead of application exception |~ 
    /// </revision>
    [Controller]
    [Authorize]
    [ServiceFilter(typeof(ControllerActionLogFilter))]
    [ServiceFilter(typeof(ControllerActionUserFilter))]
    [ServiceFilter(typeof(ControllerActionPageFilter))]
    public abstract partial class WebBaseController<T> : Controller where T : WebBaseController<T>
    {
        /// <value>ILogger</value>
        protected readonly Logger _logger;
        /// <value>IWebHostEnvironment</value>
        protected readonly IWebHostEnvironment _webHostEnvironment;
        /// <value>IWebHostEnvironment</value>
        protected readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="logger">ILogger</param>
        /// <param name="webHostEnvironment">IWebHostEnvironment</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        /// <method>WebBaseController(ILogger&lt;T&gt; logger, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)</method>
        protected WebBaseController(ILogger<T> logger, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _logger = new Logger(logger);
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Global model validation method (View found in HomeSite.ClassLibrary.Razor)
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>KeyValuePair&lt;int, string&gt;</returns>
        /// <method>ValidateModel&lt;M&gt;(M model)</method>
        protected KeyValuePair<int, string> ValidateModel<M>(M model)
        {
            int key = 0;
            string value = string.Empty;

            bool isValid = TryValidateModel(model);
            if (!isValid)
            {
                foreach (var modelValue in ModelState.Values)
                {
                    var errors = modelValue.Errors;
                    if (errors.Count > 0)
                    {
                        foreach (var error in errors)
                        {
                            key++;
                            value += Tags.Brackets(error.ErrorMessage) + Tags.LineBreak();
                        }
                    }
                }
            }

            return new KeyValuePair<int, string>(key, value);
        }

        /// <summary>
        /// Global error handling
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>IActionResult</returns>
        [AllowAnonymous]
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public virtual IActionResult Error(int id)
        {
            var vm = new ErrorViewModel(id);

            string requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            vm.RequestId = requestId;

            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionFeature != null)
            {
                if (exceptionFeature.Error.GetType().IsAssignableFrom(typeof(HttpRequestException)))
                {
                    vm.StatusCode = (int)((HttpRequestException)exceptionFeature.Error).StatusCode.Value;
                    vm.StatusMessage = exceptionFeature.Error.Message;
                    _logger.Information(exceptionFeature.Error.Message + " RequestId = " + requestId);
                }
                else
                {
                    vm.Exception = exceptionFeature.Error;
                    _logger.Exception(exceptionFeature.Error, "Exception RequestId = " + requestId);
                }
            }

            return View("Error", vm);
        }
    }
}
