using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;
using WebApplication4.Models;

namespace WebApplication4
{
    /// <summary>
    /// The travel planner lodgings controller
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class LodgingsController : Controller
    {
        private TravelPlannerDatabaseEntities db = new TravelPlannerDatabaseEntities();

        private LodgingDal _lodgingDal = new LodgingDal();

        // GET: Lodgings
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

        // GET: Lodgings/Create
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
            if (ErrorMessage != null)
            {
                ViewBag.ErrorMessage = ErrorMessage;
            }
            return View("Create");
        }

        // POST: Lodgings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
                    return RedirectToAction("Create", new { ErrorMessage = ErrorMessage });
                }
                else
                {
                    _lodgingDal.CreateNewLodging(lodging.Location, lodging.StartTime, lodging.EndTime, TripId);
                    return RedirectToAction("Index");
                }
            }
            return View(lodging);
        }

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

        private string validateConflictingLodgings(AddedLodging lodging)
        {
            var overlaps = _lodgingDal.GetOverlappingLodging(lodging.StartTime, lodging.EndTime);
            string ErrorMessage = null;
            if (overlaps.Count > 0)
            {
                ErrorMessage = "The waypoint was not added because of the following conflicts:" + "\n";
                foreach (var overlap in overlaps)
                {
                    ErrorMessage += overlap.Location + " " + overlap.StartTime + " - " + overlap.EndTime + ". ";
                }
            }
            return ErrorMessage;
        }

        // GET: Lodgings/Delete/5
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

        // POST: Lodgings/Delete/5
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
