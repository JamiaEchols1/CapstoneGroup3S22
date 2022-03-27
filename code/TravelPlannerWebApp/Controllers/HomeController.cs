using System.Web.Mvc;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    /// <summary>
    ///     The home controller class
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class HomeController : Controller
    {
        /// <summary>
        ///     The login data access layer
        /// </summary>
        public LoginDal _loginDal = new LoginDal();

        /// <summary>
        ///     Default Constructor
        /// </summary>
        public HomeController()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="_loginDal">The login dal.</param>
        public HomeController(LoginDal _loginDal)
        {
            this._loginDal = _loginDal;
        }

        /// <summary>
        ///     Returns a login page for the user.
        /// </summary>
        public ActionResult Login()
        {
            LoggedUser.User = null;
            return View();
        }

        /// <summary>
        ///     Authenticates a user's login credentials and brings them to their trip homepage
        ///     if successful.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///     login action
        /// </returns>
        [HttpPost]
        [ActionName("Login")]
        [ValidateAntiForgeryToken]
        public ActionResult AuthenticateLogin([Bind(Include = "Username,Password")] UserCredentials user)
        {
            if (ModelState.IsValid)
            {
                var loggedUser = _loginDal.CheckLoginCredentials(user.Username, user.Password);

                if (loggedUser != null)
                {
                    LoggedUser.User = new User {
                        Username = user.Username,
                        Password = user.Password,
                        Id = loggedUser.Id
                    };

                    return RedirectToAction("../Trips/Index");
                }
                else
                {
                    ModelState.AddModelError(nameof(UserCredentials.Username), "Username or Password is incorrect");
                    ModelState.AddModelError(nameof(UserCredentials.Password), "Username or Password is incorrect");
                    return View();
                }
            }

            return RedirectToAction("Login");
        }
    }
}