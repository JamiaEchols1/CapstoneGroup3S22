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

        private WaypointDAL waypointDAL = new WaypointDAL();

        private TripDAL tripDAL = new TripDAL();

        // GET: Waypoints
        public ActionResult Index()
        {
            var waypoints = waypointDAL.GetWaypoints(LoggedUser.selectedTrip.Id);
            ViewBag.TripName = LoggedUser.selectedTrip.Name;
            return View(waypoints);
        }

        // GET: Waypoints/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Waypoint waypoint = db.Waypoints.Find(id);
            if (waypoint == null)
            {
                return HttpNotFound();
            }
            LoggedUser.selectedWaypoint = waypoint;
            return View(waypoint);
        }

        // GET: Waypoints/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                id = LoggedUser.selectedTrip.Id;
            }
            ViewBag.TripId = new SelectList(db.Trips, "Id", "Name");
            LoggedUser.selectedTrip = db.Trips.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.StartDate = LoggedUser.selectedTrip.StartDate;
            ViewBag.EndDate = LoggedUser.selectedTrip.EndDate;
            return View();
        }

        // POST: Waypoints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Location,DateTime")] AddedWaypoint waypoint)
        {
            if (ModelState.IsValid)
            {
                waypoint.TripId = LoggedUser.selectedTrip.Id;
                waypointDAL.CreateNewWaypoint(waypoint.Location, waypoint.StartDateTime, waypoint.EndDateTime, waypoint.TripId);
                return RedirectToAction("Index");
            }

            ViewBag.TripId = new SelectList(db.Trips, "Id", "Name", waypoint.TripId);
            return View(waypoint);
        }

        // GET: Waypoints/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Waypoint waypoint = db.Waypoints.Find(id);
            if (waypoint == null)
            {
                return HttpNotFound();
            }
            ViewBag.TripId = new SelectList(db.Trips, "Id", "Name", waypoint.TripId);
            return View(waypoint);
        }

        // POST: Waypoints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Location,DateTime,TripId")] Waypoint waypoint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(waypoint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TripId = new SelectList(db.Trips, "Id", "Name", waypoint.TripId);
            return View(waypoint);
        }

        // GET: Waypoints/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int validatedId = (int) id;
            Waypoint waypoint = WaypointDAL.FindWaypointByID(validatedId);
            if (waypoint == null)
            {
                return HttpNotFound();
            }
            return View(waypoint);
        }

        // POST: Waypoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Waypoint waypoint = WaypointDAL.FindWaypointByID(id);
            WaypointDAL.RemoveWaypoint(waypoint);
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
