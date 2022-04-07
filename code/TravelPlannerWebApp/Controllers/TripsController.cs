using System;
using System.Collections.Generic;
using System.Linq;
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
    ///     The trips controller
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class TripsController : Controller
    {
        #region Data members

        private static string ErrorMessage;
        private readonly TravelPlannerDatabaseEntities db = new TravelPlannerDatabaseEntities();

        private readonly TripDetailsViewModel viewmodel = new TripDetailsViewModel();

        private readonly TripDal _tripDal = new TripDal();

        private readonly WaypointDal _waypointDal = new WaypointDal();

        private readonly LodgingDal _lodgingDal = new LodgingDal();

        private readonly TransportationDal _transportationDal = new TransportationDal();

        #endregion

        #region Constructors

        /// <summary>
        ///     Default trips controller constructor
        /// </summary>
        public TripsController()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TripsController" /> class.
        /// </summary>
        /// <param name="tripDal">The trip dal.</param>
        /// <param name="waypointDal">The waypoint dal.</param>
        /// <param name="lodgingDal">The lodging dal.</param>
        /// <param name="transportationDal">The transportation dal.</param>
        public TripsController(TripDal tripDal, WaypointDal waypointDal, LodgingDal lodgingDal,
            TransportationDal transportationDal)
        {
            this._tripDal = tripDal;
            this._waypointDal = waypointDal;
            this._lodgingDal = lodgingDal;
            this._transportationDal = transportationDal;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Returns a view of the trips for a user.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var trips = this._tripDal.GetTrips(LoggedUser.User.Id);
            return View(trips);
        }

        /// <summary>
        ///     Returns a view of the details of a trip for a given trip id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tripId = (int)id;
            var trip = this._tripDal.GetTripById(tripId);
            if (trip == null)
            {
                return HttpNotFound();
            }

            var waypointsAndTransportation = new List<TripItem>();
            var waypoints = this._waypointDal.GetWaypoints(trip.Id);
            var transport = this._transportationDal.GetTransportationsByTrip(trip.Id);
            foreach (var item in waypoints)
            {
                item.StartDate = item.StartDateTime;
            }

            foreach (var item in transport)
            {
                item.StartDate = item.StartTime;
            }

            waypointsAndTransportation.AddRange(waypoints);
            waypointsAndTransportation.AddRange(transport);
            this.viewmodel.WaypointsAndTransportation = waypointsAndTransportation.OrderBy(x => x.StartDate);
            this.viewmodel.Trip = trip;
            this.viewmodel.Waypoints = waypoints;
            this.viewmodel.Lodgings = this._lodgingDal.GetLodgings(trip.Id);
            this.viewmodel.Transportations = transport;
            LoggedUser.SelectedTrip = trip;

            return View(this.viewmodel);
        }

        /// <summary>
        ///     GET: Creates a new trip for a user
        /// </summary>
        public ActionResult Create()
        {
            if (ErrorMessage != null)
            {
                ViewBag.ErrorMessage = ErrorMessage;
            }

            return View();
        }

        // POST: Trips/Create
        /// <summary>
        ///     Creates the specified trip.
        /// </summary>
        /// <param name="trip">The trip.</param>
        /// <returns>
        ///     The trip view result
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,StartDate,EndDate,Description")] AddedTrip trip)
        {
            if (ModelState.IsValid)
            {
                trip.UserId = LoggedUser.User.Id;
                if (trip.StartDate < DateTime.Now)
                {
                    ErrorMessage = "Start datetime must be past the current datetime";
                    return RedirectToAction("Create");
                }

                this._tripDal.CreateNewTrip(trip.Name, trip.StartDate, trip.EndDate, trip.UserId, trip.Description);
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(this.db.Users, "Id", "Username", trip.UserId);
            LoggedUser.SelectedTrip = new Trip {
                UserId = trip.UserId,
                Id = trip.Id,
                EndDate = trip.EndDate,
                StartDate = trip.StartDate,
                Description = trip.Description
            };
            return View(trip);
        }

        /// <summary>
        ///     Sets the error message for testing only
        /// </summary>
        /// <param name="message">The message.</param>
        public void SetErrorMessage(string message)
        {
            ErrorMessage = message;
        }

        /// <summary>
        ///     GET: Returns a view of a selected trip so that the user can confirm the deletion
        ///     of the trip
        /// </summary>
        /// <param name="id">The identifier.</param>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var validatedId = (int)id;
            var trip = this._tripDal.GetTripById(validatedId);
            if (trip == null)
            {
                return HttpNotFound();
            }

            return View("Delete", trip);
        }

        /// <summary>
        ///     POST: Trip/Delete. Deletes the confirmed trip
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     The index action
        /// </returns>
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            this._tripDal.RemoveTrip(id);
            return RedirectToAction("Index");
        }

        #endregion
    }
}