using System.Web.Mvc;
using TravelPlannerLibrary;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    /// <summary>
    /// The login controller
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class LoginController : Controller
    {
        #region Data members

        private TravelPlannerDatabaseEntities db = new TravelPlannerDatabaseEntities();

        #endregion

        #region Methods

        // GET: checkLoginCredentials
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>
        /// The view
        /// </returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Authenticates this instance.
        /// </summary>
        /// <returns>
        /// The view
        /// </returns>
        public ActionResult Authenticate()
        {
            return View();
        }

        // POST: Authenticate User
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Authenticates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// The authenticate action
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Authenticate([Bind(Include = "Username,Password")] UserCredentials user)
        {
            if (ModelState.IsValid)
            {
                var loginDal = new LoginDal();
                var loggedUser = loginDal.CheckLoginCredentials(user.Username, user.Password);

                if (loggedUser != null)
                {
                    LoggedUser.User = new User {
                        Username = user.Username,
                        Password = user.Password,
                        Id = loggedUser.Id
                    };
                    return RedirectToAction("../Trips/Index");
                }
            }

            return RedirectToAction("Authenticate");
        }

        #endregion
    }
}