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

namespace WebApplication4
{
    /// <summary>
    ///     The travel planner lodgings controller
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class LodgingsController : Controller
    {
        private TravelPlannerDatabaseEntities db = new TravelPlannerDatabaseEntities();

        private LodgingDal _lodgingDal = new LodgingDal();
        private TripDal _tripDal = new TripDal();

        private static AddedLodging conflictingLodging;

        /// <summary>
        /// Initializes a new instance of the <see cref="LodgingsController"/> class.
        /// </summary>
        public LodgingsController()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LodgingsController"/> class.
        /// </summary>
        /// <param name="tripDal">The trip dal.</param>
        /// <param name="lodgingDal">The lodging dal.</param>
        public LodgingsController(TripDal tripDal, LodgingDal lodgingDal)
        {
            this._lodgingDal = lodgingDal;
            this._tripDal = tripDal;
        }

        /// <summary>
        ///     Displays all of the users lodging for a selected trip
        /// </summary>
        /// <returns>
        ///     View of all of the lodging points for a selected trip
        /// </returns>
        public ActionResult Index()
        {
            var lodgings = _lodgingDal.GetLodgings(LoggedUser.SelectedTrip.Id);
            ViewBag.TripName = LoggedUser.SelectedTrip.Name;
            return View("Index", lodgings);
        }

        /// <summary>
        ///     Details of the lodging point by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     View of lodging details
        /// </returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int lodgingId = (int)id;
            Lodging lodging = _lodgingDal.GetLodgingById(lodgingId);
            if (lodging == null)
            {
                return HttpNotFound();
            }
            LoggedUser.SelectedLodging = lodging;
            string mapURL = "https://www.google.com/maps/embed/v1/place?key=AIzaSyDJEezkTFgj0PAnzJQJVVEhfZbpUmH27s0";
            string waypointLocation = HttpUtility.UrlEncode(lodging.Location);
            ViewBag.url = mapURL + "&q=" + waypointLocation;
            return View(lodging);
        }

        /// <summary>
        ///     Prompts the user to create a new lodging point
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="ErrorMessage">The error message.</param>
        public ActionResult Create(int? id, string ErrorMessage)
        {
            if (id == null)
            {
                id = LoggedUser.SelectedTrip.Id;
            }
            LoggedUser.SelectedTrip = db.Trips.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.TripDetails = LoggedUser.SelectedTrip.Name + " " + LoggedUser.SelectedTrip.StartDate + " - " + LoggedUser.SelectedTrip.EndDate;
            ViewBag.StartDate = LoggedUser.SelectedTrip.StartDate;
            ViewBag.EndDate = LoggedUser.SelectedTrip.EndDate;
            return View("Create");
        }


        /// <summary>
        ///     Creates the specified lodging point.
        /// </summary>
        /// <param name="lodging">The lodging.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Location,StartTime,EndTime")] AddedLodging lodging)
        {
            if (ModelState.IsValid)
            {
                var TripId = LoggedUser.SelectedTrip.Id;
                string ErrorMessage = validateDateTimes(lodging);
                if (ErrorMessage == null)
                {
                    ErrorMessage = validateConflictingLodgings(lodging);
                }
                if (ErrorMessage != null)
                {
                    conflictingLodging = new AddedLodging()
                    {
                        Location = lodging.Location,
                        StartTime = lodging.StartTime,
                        EndTime = lodging.EndTime,
                        TripId = TripId
                    };
                    return RedirectToAction("CreateWithConflicts", new { ErrorMessage });
                }
                else
                {
                    _lodgingDal.CreateNewLodging(lodging.Location, lodging.StartTime, lodging.EndTime, TripId);
                    return RedirectToAction("Index");
                }
            }
            return View(lodging);
        }

        /// <summary>
        ///     Prompts the user to create the lodging point after displaying conflicting lodging.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="ErrorMessage">The error message.</param>
        public ActionResult CreateWithConflicts(int? id, string ErrorMessage)
        {
            if (id == null)
            {
                id = LoggedUser.SelectedTrip.Id;
            }
            AddedLodging Lodging = conflictingLodging;
            conflictingLodging = null;
            LoggedUser.SelectedTrip = db.Trips.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.TripDetails = LoggedUser.SelectedTrip.Name + " " + LoggedUser.SelectedTrip.StartDate + " - " + LoggedUser.SelectedTrip.EndDate;
            ViewBag.StartDate = LoggedUser.SelectedTrip.StartDate;
            ViewBag.EndDate = LoggedUser.SelectedTrip.EndDate;
            ViewBag.ErrorMessage = ErrorMessage;
            return View("CreateWithConflicts", Lodging);
        }

        /// <summary>
        ///     Creates the Lodging after displaying conflicting lodging.
        /// </summary>
        /// <param name="lodging">The lodging.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateWithConflicts([Bind(Include = "Location,StartTime,EndTime")] AddedLodging lodging)
        {
            if (ModelState.IsValid)
            {
                var TripId = LoggedUser.SelectedTrip.Id;
                _lodgingDal.CreateNewLodging(lodging.Location, lodging.StartTime, lodging.EndTime, TripId);
                return RedirectToAction("Index"); 
            }
            return View(lodging);
        }

        /// <summary>
        ///     Validates the date times for a lodging point.
        /// </summary>
        /// <param name="lodging">The lodging.</param>
        /// <returns>
        ///     ErrorMessage of invalid dates
        /// </returns>
        private string validateDateTimes(AddedLodging lodging)
        {
            string ErrorMessage = null;
            if (lodging.StartTime.CompareTo(lodging.EndTime) > 0)
            {
                ErrorMessage = "The start date must be before the end date";
            }
            if (lodging.EndTime.CompareTo(lodging.StartTime) < 0)
            {
                ErrorMessage = "The end date must be after the start date";
            }
            return ErrorMessage;
        }

        /// <summary>
        ///     Checks if there are conflicting lodgings for a given lodging point.
        /// </summary>
        /// <param name="lodging">The lodging.</param>
        /// <returns>
        ///     ErrorMessage of the conflicting lodgings
        /// </returns>
        private string validateConflictingLodgings(AddedLodging lodging)
        {
            var overlaps = _lodgingDal.GetOverlappingLodging(lodging.StartTime, lodging.EndTime);
            string ErrorMessage = null;
            if (overlaps.Count > 0)
            {
                ErrorMessage = "The lodging was not added because of the following conflicts:" + "\n";
                foreach (var overlap in overlaps)
                {
                    ErrorMessage += overlap.Location + " " + overlap.StartTime + " - " + overlap.EndTime + ". ";
                }
                ErrorMessage += "Select \"Confirm With Conflicts\" to add the lodging anyways.";
            }
            return ErrorMessage;
        }

        /// <summary>
        ///     Prompts the user to delete the specified lodging.
        /// </summary>
        /// <param name="id">The identifier of the lodging.</param>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int lodgingId = (int)id;
            Lodging lodging = _lodgingDal.GetLodgingById(lodgingId);
            if (lodging == null)
            {
                return HttpNotFound();
            }
            return View(lodging);
        }


        /// <summary>
        ///     Deletes the confirmed lodging.
        /// </summary>
        /// <param name="id">The identifier of the lodging.</param>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int lodgingId = (int)id;
            Lodging lodging = _lodgingDal.GetLodgingById(lodgingId);
            _lodgingDal.RemoveLodging(lodging);
            return RedirectToAction("Index");
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
