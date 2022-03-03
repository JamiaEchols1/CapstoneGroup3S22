using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApplication4
{
    /// <summary>
    ///     The mvc application
    /// </summary>
    /// <seealso cref="System.Web.HttpApplication" />
    public class MvcApplication : HttpApplication
    {
        #region Methods

        /// <summary>
        ///     The application start
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        #endregion
    }
}