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
        public ActionResult Login()
        {
            LoggedUser.User = null;
            return View();
        }

        /// <summary>
        ///     Authenticates the login.
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