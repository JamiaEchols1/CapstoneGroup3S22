using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TravelPlannerLibrary;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    /// <summary>
    ///     The waypoints controller
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class WaypointsController : Controller
    {
        #region Data members

        private readonly TravelPlannerDatabaseEntities db = new TravelPlannerDatabaseEntities();

        private readonly WaypointDal waypointDal = new WaypointDal();

        private TripDal tripDal = new TripDal();

        #endregion

        #region Methods

        // GET: Waypoints
        /// <summary>
        ///     Indexes this instance.
        /// </summary>
        /// <returns>
        ///     the waypoints view result
        /// </returns>
        public ActionResult Index()
        {
            var waypoints = this.waypointDal.GetWaypoints(LoggedUser.SelectedTrip.Id);
            ViewBag.TripName = LoggedUser.SelectedTrip.Name;
            return View(waypoints);
        }

        // GET: Waypoints/Details/5
        /// <summary>
        ///     Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     the waypoint view result
        /// </returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var waypoint = this.db.Waypoints.Find(id);
            if (waypoint == null)
            {
                return HttpNotFound();
            }

            LoggedUser.SelectedWaypoint = waypoint;
            return View(waypoint);
        }

        // GET: Waypoints/Create
        /// <summary>
        ///     Creates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     The rendered view
        /// </returns>
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                id = LoggedUser.SelectedTrip.Id;
            }

            ViewBag.TripId = new SelectList(this.db.Trips, "Id", "Name");
            LoggedUser.SelectedTrip = this.db.Trips.FirstOrDefault(x => x.Id == id);
            ViewBag.StartDate = LoggedUser.SelectedTrip.StartDate;
            ViewBag.EndDate = LoggedUser.SelectedTrip.EndDate;
            return View();
        }

        // POST: Waypoints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        ///     Creates the specified waypoint.
        /// </summary>
        /// <param name="waypoint">The waypoint.</param>
        /// <returns>
        ///     The index view result
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Location,DateTime")] AddedWaypoint waypoint)
        {
            if (ModelState.IsValid)
            {
                waypoint.TripId = LoggedUser.SelectedTrip.Id;
                this.waypointDal.CreateNewWaypoint(waypoint.Location, waypoint.StartDateTime, waypoint.EndDateTime,
                    waypoint.TripId, waypoint.Description);
                return RedirectToAction("Index");
            }

            ViewBag.TripId = new SelectList(this.db.Trips, "Id", "Name", waypoint.TripId);
            return View(waypoint);
        }

        // GET: Waypoints/Edit/5
        /// <summary>
        ///     Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     The waypoint view result
        /// </returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var waypoint = this.db.Waypoints.Find(id);
            if (waypoint == null)
            {
                return HttpNotFound();
            }

            ViewBag.TripId = new SelectList(this.db.Trips, "Id", "Name", waypoint.TripId);
            return View(waypoint);
        }

        // POST: Waypoints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        ///     Edits the specified waypoint.
        /// </summary>
        /// <param name="waypoint">The waypoint.</param>
        /// <returns>
        ///     The index action result
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Location,DateTime,TripId")] Waypoint waypoint)
        {
            if (ModelState.IsValid)
            {
                this.db.Entry(waypoint).State = EntityState.Modified;
                this.db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TripId = new SelectList(this.db.Trips, "Id", "Name", waypoint.TripId);
            return View(waypoint);
        }

        // GET: Waypoints/Delete/5
        /// <summary>
        ///     Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     The waypoint action result
        /// </returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var validatedId = (int)id;
            var waypoint = this.waypointDal.GetWaypoint(validatedId);
            if (waypoint == null)
            {
                return HttpNotFound();
            }

            return View(waypoint);
        }

        // POST: Waypoints/Delete/5
        /// <summary>
        ///     Deletes the confirmed.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     The index action result
        /// </returns>
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var waypoint = this.waypointDal.GetWaypoint(id);
            this.waypointDal.RemoveWaypoint(waypoint);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Releases unmanaged resources and optionally releases managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}