using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelPlannerLibrary;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class LoginController : Controller
    {
        private TravelPlannerDatabaseEntities db = new TravelPlannerDatabaseEntities();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Authenticate()
        {
            return View();
        }

        // POST: Authenticate User
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Authenticate([Bind(Include = "Username,Password")] UserCredentials user)
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


            }

            return RedirectToAction("Authenticate");
        }
    }
}