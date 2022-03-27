using System;
using System.Net;
using System.Web.Mvc;
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
        private TravelPlannerDatabaseEntities db = new TravelPlannerDatabaseEntities();

        private TripDetailsViewModel viewmodel = new TripDetailsViewModel();

        private TripDal _tripDal = new TripDal();

        private WaypointDal _waypointDal = new WaypointDal();

        private LodgingDal _lodgingDal = new LodgingDal();

        private static string ErrorMessage;

        /// <summary>
        ///     Default trips controller constructor
        /// </summary>
        public TripsController()
        {

        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TripsController"/> class.
        /// </summary>
        /// <param name="tripDal">The trip dal.</param>
        /// <param name="waypointDal">The waypoint dal.</param>
        /// <param name="lodgingDal">The lodging dal.</param>
        public TripsController(TripDal tripDal, WaypointDal waypointDal, LodgingDal lodgingDal)
        {
            _tripDal = tripDal;
            _waypointDal = waypointDal;
            _lodgingDal = lodgingDal;
        }

        /// <summary>
        ///     Returns a view of the trips for a user.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var trips = _tripDal.GetTrips(LoggedUser.User.Id);
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
            int tripId = (int)id;
            Trip trip = _tripDal.GetTripById(tripId);
            if (trip == null)
            {
                return HttpNotFound();
            }
            viewmodel.Trip = trip;
            viewmodel.Waypoints = _waypointDal.GetWaypoints(trip.Id);
            viewmodel.Lodgings = _lodgingDal.GetLodgings(trip.Id);
            LoggedUser.SelectedTrip = trip;

            return View(viewmodel);
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
                _tripDal.CreateNewTrip(trip.Name, trip.StartDate, trip.EndDate, trip.UserId);
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

        /// <summary>
        ///     Sets the error message for testing only
        /// </summary>
        /// <param name="message">The message.</param>
        public void SetErrorMessage(string message)
        {
            ErrorMessage = message;
        }

        /// <summary>
        /// Releases unmanaged resources and optionally releases managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
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
