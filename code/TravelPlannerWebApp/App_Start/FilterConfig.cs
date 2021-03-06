using System.Web.Mvc;

namespace WebApplication4
{
    /// <summary>
    /// The filter config class
    /// </summary>
    public class FilterConfig
    {
        #region Methods

        /// <summary>
        /// Registers the global filters.
        /// </summary>
        /// <param name="filters">The filters.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        #endregion
    }
}