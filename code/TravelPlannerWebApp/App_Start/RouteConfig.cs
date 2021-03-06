using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication4
{
    /// <summary>
    ///     The route config class
    /// </summary>
    public class RouteConfig
    {
        #region Methods

        /// <summary>
        ///     Registers the routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            );
        }

        #endregion
    }
}