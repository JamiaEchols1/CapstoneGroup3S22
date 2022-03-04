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

        private static string ErrorMessage;

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
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                id = LoggedUser.SelectedTrip.Id;
            }
            LoggedUser.SelectedTrip = db.Trips.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.StartDate = LoggedUser.SelectedTrip.StartDate;
            ViewBag.EndDate = LoggedUser.SelectedTrip.EndDate;
            ViewBag.TripDetails = LoggedUser.SelectedTrip.Name + " " + LoggedUser.SelectedTrip.StartDate + " - " + LoggedUser.SelectedTrip.EndDate;
            if (AddedWaypoint.ConflictingWaypoints.Count > 0)
            {
                ViewBag.Overlaps = AddedWaypoint.ConflictingWaypoints;
            }
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
                var overlaps = waypointDAL.GetOverlappingWaypoints(waypoint.StartDateTime, waypoint.EndDateTime);
                if (validateConflictingWaypoints(waypoint))
                {
                    return RedirectToAction("Create", waypoint.TripId);
                }
                else if (!validateDateTimes(waypoint))
                {
                    return RedirectToAction("Create", waypoint.TripId);
                }
                else
                {
                    waypointDAL.CreateNewWaypoint(waypoint.Location, waypoint.StartDateTime, waypoint.EndDateTime, waypoint.TripId, waypoint.Description);
                    AddedWaypoint.ConflictingWaypoints = new List<Waypoint>();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ErrorMessage = null;
                AddedWaypoint.ConflictingWaypoints = new List<Waypoint>();
            }
            return View(waypoint);
        }

        private bool validateDateTimes(AddedWaypoint waypoint)
        {
            bool isValid = true;
            if (waypoint.StartDateTime >= waypoint.EndDateTime)
            {
                ErrorMessage = "The start date must be before than the end date";
                isValid = false;
            }
            else if (waypoint.EndDateTime <= waypoint.StartDateTime)
            {
                ErrorMessage = "The end date must be after than the start date";
                isValid = false;
            }
            else if (waypoint.EndDateTime.CompareTo(LoggedUser.SelectedTrip.EndDate) > 0)
            {
                ErrorMessage = "End date must be on or before trip end date";
                isValid = false;
            }
            else if (waypoint.StartDateTime.CompareTo(LoggedUser.SelectedTrip.EndDate) > 0)
            {
                ErrorMessage = "Start date must be on or before trip end date";
                isValid = false;
            }
            else
            {
                ErrorMessage = null;
            }
            return isValid;
        }

        private bool validateConflictingWaypoints(AddedWaypoint waypoint)
        {
            var overlaps = waypointDAL.GetOverlappingWaypoints(waypoint.StartDateTime, waypoint.EndDateTime);
            bool hasConflicts = false;
            if (overlaps.Count > 0)
            {
                AddedWaypoint.ConflictingWaypoints = overlaps;
                hasConflicts = true;
            }
            else
            {
                AddedWaypoint.ConflictingWaypoints = new List<Waypoint>();
            }
            return hasConflicts;
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
