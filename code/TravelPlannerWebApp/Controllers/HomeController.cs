using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            LoggedUser.user = null; 
            return View();
        }

        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        public ActionResult AuthenticateLogin([Bind(Include = "Username,Password")] UserCredentials user)
        {
            if (ModelState.IsValid)
            {
                LoginDAL loginDal = new LoginDAL();
                var loggedUser = loginDal.Login(user.Username, user.Password);

                if (loggedUser != null)
                {
                    LoggedUser.user = new TravelPlannerLibrary.Models.User()
                    {
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