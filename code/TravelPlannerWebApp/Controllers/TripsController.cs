using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using TravelPlannerLibrary;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;
using WebApplication4.Models;
using WebApplication4.ViewModels;

namespace WebApplication4.Controllers
{
    /// <summary>
    /// The trip controller
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class TripsController : Controller
    {
        #region Data members

        private readonly TravelPlannerDatabaseEntities db = new TravelPlannerDatabaseEntities();

        private readonly TripDetailsViewModel viewmodel = new TripDetailsViewModel();

        private readonly TripDal tripDal = new TripDal();

        private readonly WaypointDal waypointDal = new WaypointDal();

        #endregion

        #region Methods

        // GET: Trips
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>
        /// the trips view result
        /// </returns>
        public ActionResult Index()
        {
            var trips = this.tripDal.GetTrips(LoggedUser.User.Id);
            return View(trips);
        }

        // GET: Trips/Details/5
        /// <summary>
        /// Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The viewmodel view result
        /// </returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var trip = this.db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }

            this.viewmodel.Trip = trip;
            this.viewmodel.Waypoints = this.waypointDal.GetWaypoints(trip.Id);
            LoggedUser.SelectedTrip = trip;

            return View(this.viewmodel);
        }

        // GET: Trips/Create
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>
        /// The rendered view result
        /// </returns>
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(this.db.Users, "Id", "Username");
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
        /// the trip view result
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,StartDate,EndDate")] AddedTrip trip)
        {
            if (ModelState.IsValid)
            {
                trip.UserId = LoggedUser.User.Id;
                this.tripDal.CreateNewTrip(trip.Name, trip.StartDate, trip.EndDate, trip.UserId);
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(this.db.Users, "Id", "Username", trip.UserId);
            LoggedUser.SelectedTrip = new Trip {
                UserId = trip.UserId,
                Id = trip.Id,
                EndDate = trip.EndDate,
                StartDate = trip.StartDate
            };
            return View(trip);
        }

        // GET: Trips/Edit/5
        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The trip view result
        /// </returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var trip = this.db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }

            ViewBag.UserId = new SelectList(this.db.Users, "Id", "Username", trip.UserId);
            return View(trip);
        }

        // POST: Trips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edits the specified trip.
        /// </summary>
        /// <param name="trip">The trip.</param>
        /// <returns>
        /// The index action result
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,StartDate,EndDate,UserId")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                this.db.Entry(trip).State = EntityState.Modified;
                this.db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(this.db.Users, "Id", "Username", trip.UserId);
            return View(trip);
        }

        // GET: Trips/Delete/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The trip view result
        /// </returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var trip = this.db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }

            return View(trip);
        }

        // POST: Trips/Delete/5
        /// <summary>
        /// Deletes the confirmed.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The index action result
        /// </returns>
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            this.tripDal.RemoveTrip(id);
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