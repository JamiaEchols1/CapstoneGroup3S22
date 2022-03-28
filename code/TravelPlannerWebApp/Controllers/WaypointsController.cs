using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    /// <summary>
    ///     The travel planner web waypoints controller
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class WaypointsController : Controller
    {
        private readonly WaypointDal _waypointDal = new WaypointDal();
        private readonly TransportationDal _transportationDal = new TransportationDal();

        private readonly TripDal _tripDal = new TripDal();

        /// <summary>
        /// Initializes a new instance of the <see cref="WaypointsController"/> class.
        /// Default Constructor
        /// </summary>
        public WaypointsController()
        {            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WaypointsController"/> class.
        /// Default Constructor
        /// </summary>
        public WaypointsController(TripDal tripDal, WaypointDal waypointDal)
        {
            _waypointDal = waypointDal;
            _tripDal = tripDal;
        }

        /// <summary>
        ///     GET: Returns a view of the waypoints for a selected trip
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var waypoints = _waypointDal.GetWaypoints(LoggedUser.SelectedTrip.Id);
            ViewBag.TripName = LoggedUser.SelectedTrip.Name;
            return View("Index", waypoints);
        }

        /// <summary>
        ///     GET: Returns a view of a specific waypoint's details for a selected trip
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int waypointId = (int) id;
            Waypoint waypoint = _waypointDal.GetWaypoint(waypointId);
            if (waypoint == null)
            {
                return HttpNotFound();
            }
            LoggedUser.SelectedWaypoint = waypoint;
            string mapURL = "https://www.google.com/maps/embed/v1/place?key=AIzaSyDJEezkTFgj0PAnzJQJVVEhfZbpUmH27s0";
            string waypointLocation = HttpUtility.UrlEncode(waypoint.Location);
            ViewBag.url = mapURL + "&q=" + waypointLocation;
            return View("Details", waypoint);
        }

        /// <summary>
        ///     GET: Returns a view for a user to create a new waypoint for a selected trip
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="ErrorMessage">The error message.</param>
        public ActionResult Create(int? id, string ErrorMessage)
        {
            if (id == null)
            {
                id = LoggedUser.SelectedTrip.Id;
            }
            var waypointId = (int)id;
            LoggedUser.SelectedTrip = _waypointDal.GetTripFromWaypoint(waypointId);
            ViewBag.TripDetails = LoggedUser.SelectedTrip.Name + " " + LoggedUser.SelectedTrip.StartDate + " - " + LoggedUser.SelectedTrip.EndDate;
            ViewBag.StartDate = LoggedUser.SelectedTrip.StartDate;
            ViewBag.EndDate = LoggedUser.SelectedTrip.EndDate;
            if (ErrorMessage != null)
            {
                ViewBag.ErrorMessage = ErrorMessage;
            }
            return View("Create");
        }

        /// <summary>
        ///     POST: Waypoints/Create. Creates the specified waypoint.
        /// </summary>
        /// <param name="waypoint">The waypoint.</param>
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
                    ErrorMessage = validateConflictingTransportAndWaypoints(waypoint);
                }
                if (ErrorMessage != null)
                {
                    return RedirectToAction("Create", new { ErrorMessage = ErrorMessage});
                }
                else
                {
                    _waypointDal.CreateNewWaypoint(waypoint.Location, waypoint.StartDateTime, waypoint.EndDateTime, waypoint.TripId, waypoint.Description);
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
        private string validateConflictingTransportAndWaypoints(AddedWaypoint waypoint)
        {
            string ErrorMessage = null;
            var startDate = waypoint.StartDateTime;
            var endDate = waypoint.EndDateTime;
            var waypointsAndTransportation = new List<object>();
            waypointsAndTransportation.AddRange(this._waypointDal.GetOverlappingWaypoints(startDate, endDate));
            waypointsAndTransportation.AddRange(this._transportationDal.GetOverlappingTransportation(startDate, endDate));
            if (waypointsAndTransportation.Count > 0)
            {
                ErrorMessage = "The transportation was not added because of the following conflicts:" + "\n";
                foreach (var overlap in waypointsAndTransportation)
                {
                    ErrorMessage += overlap.ToString();
                }
            }
            return ErrorMessage;
        }


        /// <summary>
        ///     GET: Returns a view of a selected waypoint so that the user can confirm the deletion
        ///     of the waypoint
        /// </summary>
        /// <param name="id">The identifier.</param>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int validatedId = (int) id;
            Waypoint waypoint = _waypointDal.GetWaypoint(validatedId);
            if (waypoint == null)
            {
                return HttpNotFound();
            }
            return View("Delete", waypoint);
        }

        /// <summary>
        ///     POST: Waypoints/Delete. Deletes the confirmed waypoint
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///  The index action
        /// </returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Waypoint waypoint = _waypointDal.GetWaypoint(id);
            this._waypointDal.RemoveWaypoint(waypoint);
            return RedirectToAction("Index");
        }
    }
}
