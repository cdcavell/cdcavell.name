using CDCavell.ClassLibrary.Web.Mvc.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace cdcavell.Controllers
{
    /// <summary>
    /// Administration Controller
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 10/03/2020 | Initial build |~ 
    /// </revision>
    public class AdministrationController : WebBaseController<AdministrationController>
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="logger">ILogger&lt;AdministrationController&gt;</param>
        /// <param name="webHostEnvironment">IWebHostEnvironment</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        /// <method>
        /// AccountController(
        ///     ILogger&lt;AdministrationController&gt; logger, 
        ///     IWebHostEnvironment webHostEnvironment, 
        ///     IHttpContextAccessor httpContextAccessor
        /// ) : base(logger, webHostEnvironment, httpContextAccessor)
        /// </method>
        public AdministrationController(
            ILogger<AdministrationController> logger,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor
        ) : base(logger, webHostEnvironment, httpContextAccessor)
        {
        }

        /// <summary>
        /// Index method
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <method>Index()</method>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

    }
}
