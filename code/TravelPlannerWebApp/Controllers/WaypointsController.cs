using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using TravelPlannerLibrary;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;
using WebApplication4.Models;
using WebApplication4.ViewModels;

namespace WebApplication4.Controllers
{
    public class WaypointsController : Controller
    {
        private TravelPlannerDatabaseEntities db = new TravelPlannerDatabaseEntities();

        private readonly WaypointDal waypointDAL = new WaypointDal();

        private readonly TripDal tripDAL = new TripDal();

        // GET: Waypoints
        public ActionResult Index()
        {
            var waypoints = waypointDAL.GetWaypoints(LoggedUser.SelectedTrip.Id);
            ViewBag.TripName = LoggedUser.SelectedTrip.Name;
            return View("Index", waypoints);
        }

        // GET: Waypoints/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int waypointId = (int) id;
            Waypoint waypoint = waypointDAL.GetWaypoint(waypointId);
            if (waypoint == null)
            {
                return HttpNotFound();
            }
            LoggedUser.SelectedWaypoint = waypoint;
            return View("Details", waypoint);
        }

        // GET: Waypoints/Create
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

        // POST: Waypoints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Creates the specified waypoint.
        /// </summary>
        /// <param name="waypoint">The waypoint.</param>
        /// <returns>
        /// The waypoint view result
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Location,StartDateTime,EndDateTime,Description")] AddedWaypoint waypoint)
        {
            if (ModelState.IsValid)
            {
                waypoint.TripId = LoggedUser.SelectedTrip.Id;
                string ErrorMessage = validateDateTimes(waypoint);
                if (ErrorMessage == null)
                {
                    ErrorMessage = validateConflictingWaypoints(waypoint);
                }
                if (ErrorMessage != null)
                {
                    return RedirectToAction("Create", new { ErrorMessage = ErrorMessage});
                }
                else
                {
                    waypointDAL.CreateNewWaypoint(waypoint.Location, waypoint.StartDateTime, waypoint.EndDateTime, waypoint.TripId, waypoint.Description);
                    return RedirectToAction("Index");
                }
            }
            return View(waypoint);
        }

        private string validateDateTimes(AddedWaypoint waypoint)
        {
            string ErrorMessage = null;
            if (waypoint.StartDateTime.CompareTo(waypoint.EndDateTime) > 0)
            {
                ErrorMessage = "The start date must be before the end date";
            }
            if (waypoint.EndDateTime.CompareTo(waypoint.StartDateTime) < 0)
            {
                ErrorMessage = "The end date must be after the start date";
            }
            return ErrorMessage;
        }

        private string validateConflictingWaypoints(AddedWaypoint waypoint)
        {
            var overlaps = waypointDAL.GetOverlappingWaypoints(waypoint.StartDateTime, waypoint.EndDateTime);
            string ErrorMessage = null;
            if (overlaps.Count > 0)
            {
                ErrorMessage = "The waypoint was not added because of the following conflicts:" + "\n";
                foreach (var overlap in overlaps)
                {
                    ErrorMessage += overlap.Location + " " + overlap.StartDateTime + " - " + overlap.EndDateTime + ". ";
                }
            }
            return ErrorMessage;
        }

        // GET: Waypoints/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int validatedId = (int) id;
            Waypoint waypoint = waypointDAL.GetWaypoint(validatedId);
            if (waypoint == null)
            {
                return HttpNotFound();
            }
            return View("Delete", waypoint);
        }

        // POST: Waypoints/Delete/5
        /// <summary>
        /// Deletes the confirmed.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///  The index action
        /// </returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Waypoint waypoint = waypointDAL.GetWaypoint(id);
            this.waypointDAL.RemoveWaypoint(waypoint);
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
