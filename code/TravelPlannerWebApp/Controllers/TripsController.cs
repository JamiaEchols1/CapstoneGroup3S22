using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TravelPlannerLibrary;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;
using WebApplication4.Models;
using WebApplication4.ViewModels;

namespace WebApplication4.Controllers
{
    public class TripsController : Controller
    {
        private TravelPlannerDatabaseEntities db = new TravelPlannerDatabaseEntities();

        private TripDetailsViewModel viewmodel = new TripDetailsViewModel();

        private TripDal tripDAL = new TripDal();

        private WaypointDal waypointDAL = new WaypointDal();

        private static string ErrorMessage;

        // GET: Trips
        public ActionResult Index()
        {
            var trips = tripDAL.GetTrips(LoggedUser.User.Id);
            return View(trips);
        }

        // GET: Trips/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            viewmodel.Trip = trip;
            viewmodel.Waypoints = waypointDAL.GetWaypoints(trip.Id);
            LoggedUser.SelectedTrip = trip;

            return View(viewmodel);
        }

        // GET: Trips/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username");
            if (ErrorMessage != null)
            {
                ViewBag.ErrorMessage = ErrorMessage;
            }
            return View();
        }

        // POST: Trips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Creates the specified trip.
        /// </summary>
        /// <param name="trip">The trip.</param>
        /// <returns>
        ///  The trip view result
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,StartDate,EndDate")] AddedTrip trip)
        {
            if (ModelState.IsValid)
            {
                trip.UserId = LoggedUser.User.Id;
                if (trip.StartDate < DateTime.Now)
                {
                    ErrorMessage = "Start datetime must be past the current datetime";
                    return RedirectToAction("Create");
                }
                tripDAL.CreateNewTrip(trip.Name, trip.StartDate, trip.EndDate, trip.UserId);
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", trip.UserId);
            LoggedUser.SelectedTrip = new Trip() 
            {
                UserId = trip.UserId,
                Id = trip.Id,
                EndDate = trip.EndDate,
                StartDate = trip.StartDate
            };
            return View(trip);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
