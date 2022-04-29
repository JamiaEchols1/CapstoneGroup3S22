using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;
using WebApplication4.Models;
using WebApplication4.ViewModels;

namespace WebApplication4.Controllers
{
    /// <summary>
    ///     The travel planner web waypoints controller
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class WaypointsController : Controller
    {
        #region Data members

        private readonly WaypointDal _waypointDal = new WaypointDal();
        private readonly TransportationDal _transportationDal = new TransportationDal();

        private readonly TripDal _tripDal = new TripDal();
        private static VerifyTimeViewModel verifyTimeViewModel = new VerifyTimeViewModel();

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="WaypointsController" /> class.
        ///     Default Constructor
        /// </summary>
        public WaypointsController()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="WaypointsController" /> class.
        ///     Default Constructor
        /// </summary>
        public WaypointsController(TripDal tripDal, WaypointDal waypointDal)
        {
            this._waypointDal = waypointDal;
            this._tripDal = tripDal;
        }

        #endregion

        #region Methods

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

            var waypointId = (int)id;
            var waypoint = this._waypointDal.GetWaypoint(waypointId);
            if (waypoint == null)
            {
                return HttpNotFound();
            }

            LoggedUser.SelectedWaypoint = waypoint;
            var mapURL = "https://www.google.com/maps/embed/v1/place?key=AIzaSyDJEezkTFgj0PAnzJQJVVEhfZbpUmH27s0";
            var waypointLocation = HttpUtility.UrlEncode(waypoint.Location);
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
            LoggedUser.SelectedTrip = this._waypointDal.GetTripFromWaypoint(waypointId);
            ViewBag.TripDetails = LoggedUser.SelectedTrip.Name + " " + LoggedUser.SelectedTrip.StartDate + " - " +
                                  LoggedUser.SelectedTrip.EndDate;
            ViewBag.StartDate = LoggedUser.SelectedTrip.StartDate;
            ViewBag.EndDate = LoggedUser.SelectedTrip.EndDate;
            if (ErrorMessage != null)
            {
                ViewBag.ErrorMessage = ErrorMessage;
            }

            return View("Create");
        }

        /// <summary>
        ///     POST: Waypoints/Create. Creates the specified waypoint. If there are conflicting
        ///     waypoints with the one being created, a new create waypoint view will be returned
        ///     with an error message of the conflicting waypoints.
        /// </summary>
        /// <param name="waypoint">The waypoint.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Location,StartDateTime,EndDateTime,Description")]
            AddedWaypoint waypoint)
        {
            if (ModelState.IsValid)
            {
                waypoint.TripId = LoggedUser.SelectedTrip.Id;
                var ErrorMessage = this.validateDateTimes(waypoint);
                if (ErrorMessage == null)
                {
                    ErrorMessage = this.validateConflictingTransportAndWaypoints(waypoint);
                }

                if (ErrorMessage != null)
                {
                    return RedirectToAction("Create", new { ErrorMessage });
                }

                Waypoint newWaypoint = AddedWaypoint.ConvertAddedWaypointToWaypoint(waypoint);
                var waypointsAndTransportation = this.getTripItemsWithNew(newWaypoint);
                int newIndex = waypointsAndTransportation.IndexOf(newWaypoint);

                TripItem previous = null;
                if (newIndex != 0)
                {
                    previous =
                        waypointsAndTransportation.ElementAt(newIndex - 1);
                }

                if (previous != null)
                {
                    var timeDiffPrev = waypoint.StartDateTime - previous.EndDate;
                    var estimatedTimePrev = this.calcEstimatedTime(waypoint, previous);
                    if (estimatedTimePrev > timeDiffPrev)
                    {
                        VerifyTimeViewModel model = new VerifyTimeViewModel(estimatedTimePrev, timeDiffPrev, waypoint, previous, true);
                        WaypointsController.verifyTimeViewModel = model;
                        return View("CreateWithTimeVerification", model);
                    }
                }

                TripItem next = null;
                if (waypointsAndTransportation.Count > newIndex + 1)
                {
                    next = waypointsAndTransportation.ElementAt(newIndex + 1);
                }

                if (next != null)
                {
                    var timeDiffNext = next.StartDate - waypoint.EndDateTime;
                    var estimatedTimeNext = this.calcEstimatedTime(waypoint, next);
                    if (estimatedTimeNext > timeDiffNext)
                    {
                        VerifyTimeViewModel model = new VerifyTimeViewModel(estimatedTimeNext, timeDiffNext, waypoint, next, false);
                        WaypointsController.verifyTimeViewModel = model;
                        return View("CreateWithTimeVerification", model);
                    }
                }


                this._waypointDal.CreateNewWaypoint(waypoint.Location, waypoint.StartDateTime, waypoint.EndDateTime,
                waypoint.TripId, waypoint.Description);
                return RedirectToAction("../Trips/Details", new { id = LoggedUser.SelectedTrip.Id });
            }

                return View(waypoint);
        }

        private List<TripItem> getTripItemsWithNew(Waypoint newWaypoint)
        {
            var waypointsAndTransportation = new List<TripItem>();
            var waypoints = this._waypointDal.GetWaypoints(LoggedUser.SelectedTrip.Id);
            waypoints.Add(newWaypoint);
            var transport = this._transportationDal.GetTransportationsByTrip(LoggedUser.SelectedTrip.Id);
            foreach (var item in waypoints)
            {
                item.StartDate = item.StartDateTime;
                item.EndDate = item.EndDateTime;
            }

            foreach (var item in transport)
            {
                item.StartDate = item.StartTime;
                item.EndDate = item.EndTime;
            }

            waypointsAndTransportation.AddRange(waypoints);
            waypointsAndTransportation.AddRange(transport);
            return waypointsAndTransportation.OrderBy(x => x.StartDate).ToList();
        }

        private TimeSpan calcEstimatedTime(AddedWaypoint waypoint, TripItem tripItem)
        {
            var originLocation = HttpUtility.UrlEncode(waypoint.Location);
            string destinationLocation;
            if (tripItem.GetType() == typeof(Waypoint))
            {
                Waypoint prev = (Waypoint)tripItem;
                destinationLocation = HttpUtility.UrlEncode(prev.Location);
            }
            else
            {
                Transportation prev = (Transportation)tripItem;
                destinationLocation = HttpUtility.UrlEncode(prev.Destination);
            }

            string url = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" +
                         originLocation +
                         "&destinations=" +
                         destinationLocation +
                         "&key=AIzaSyDJEezkTFgj0PAnzJQJVVEhfZbpUmH27s0";
            WebRequest request = WebRequest.Create(url);
            using (WebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    DataSet dsResult = new DataSet();
                    dsResult.ReadXml(reader);
                    return TimeSpan.FromSeconds(Int32.Parse(dsResult.Tables["duration"].Rows[0]["value"].ToString()));
                }
            }
        }

        private string validateDateTimes(AddedWaypoint waypoint)
        {
            string ErrorMessage = null;
            if (waypoint.StartDateTime.CompareTo(waypoint.EndDateTime) > 0)
            {
                ErrorMessage = "The start date must be before the end date";
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
            waypointsAndTransportation.AddRange(
                this._transportationDal.GetOverlappingTransportation(startDate, endDate));
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

        private string validateConflictingTransportAndWaypointsForEditedWaypoint(Waypoint waypoint)
        {
            string ErrorMessage = null;
            var startDate = waypoint.StartDateTime;
            var endDate = waypoint.EndDateTime;
            var waypointsAndTransportation = new List<object>();
            waypointsAndTransportation.AddRange(this._waypointDal.GetOverlappingWaypointsForUpdatedWaypoint(startDate, endDate, waypoint));
            waypointsAndTransportation.AddRange(
                this._transportationDal.GetOverlappingTransportation(startDate, endDate));
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

            var validatedId = (int)id;
            var waypoint = this._waypointDal.GetWaypoint(validatedId);
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
        ///     The index action
        /// </returns>
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var waypoint = this._waypointDal.GetWaypoint(id);
            this._waypointDal.RemoveWaypoint(waypoint);
            return RedirectToAction("../Trips/Details", new { id = LoggedUser.SelectedTrip.Id });
        }

        /// <summary>
        /// GET: Edits the specified waypoint by id.
        /// </summary>
        /// <param name="id">The waypoint identifier.</param>
        /// <param name="ErrorMessage">The error message.</param>
        /// <returns>
        /// View of waypoint details if found, HttpNotFound else
        /// </returns>
        public ActionResult Edit(int? id, string ErrorMessage)
        {
            int waypointId = (int)id;
            Waypoint waypoint = this._waypointDal.GetWaypoint(waypointId);
            LoggedUser.SelectedTrip = this._waypointDal.GetTripFromWaypoint(waypoint.TripId);
            AddedWaypoint editedWaypoint = AddedWaypoint.ConvertWaypointToAddedWaypoint(waypoint);
            ViewBag.TripDetails = LoggedUser.SelectedTrip.Name + " " + LoggedUser.SelectedTrip.StartDate + " - " +
                                  LoggedUser.SelectedTrip.EndDate;
            ViewBag.StartDate = LoggedUser.SelectedTrip.StartDate;
            ViewBag.EndDate = LoggedUser.SelectedTrip.EndDate;
            if (ErrorMessage != null)
            {
                ViewBag.ErrorMessage = ErrorMessage;
            }
            return View(editedWaypoint);
        }

        /// <summary>
        ///     POST: Edits the specified waypoint.
        /// </summary>
        /// <param name="waypoint">The waypoint.</param>
        /// <returns>
        ///     Trips details if successful, edit view else
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Location,StartDateTime,EndDateTime,TripId,Description")] AddedWaypoint waypoint)
        {
            if (ModelState.IsValid)
            {
                Waypoint editedWaypoint = AddedWaypoint.ConvertAddedWaypointToWaypoint(waypoint);
                var ErrorMessage = this.validateDateTimes(waypoint);
                if (ErrorMessage == null)
                {
                    ErrorMessage = this.validateConflictingTransportAndWaypointsForEditedWaypoint(editedWaypoint);
                }

                if (ErrorMessage != null)
                {
                    return RedirectToAction("Edit", new { ErrorMessage });
                }
                int tripID = LoggedUser.SelectedTrip.Id;
                waypoint.TripId = tripID;
                this._waypointDal.WebUpdateWaypoint(editedWaypoint);
                return RedirectToAction("Details", new { id = editedWaypoint.Id });
            }
            return View(waypoint);
        }


        /// <summary>
        /// Creates the with time verification.
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateWithTimeVerification()
        {
            return View();
        }

        /// <summary>
        ///     POST: Waypoints/Create. Creates the specified waypoint. If there are conflicting
        ///     waypoints with the one being created, a new create waypoint view will be returned
        ///     with an error message of the conflicting waypoints.
        /// </summary>
        /// <param name="waypoint">The waypoint.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateWithTimeVerification(
            [Bind(Include = "waypoint")]
            VerifyTimeViewModel model)
        {
            model = verifyTimeViewModel;
            model.waypoint.TripId = LoggedUser.SelectedTrip.Id;
            var ErrorMessage = this.validateDateTimes(model.waypoint);
            if (ErrorMessage == null)
            {
                ErrorMessage = this.validateConflictingTransportAndWaypoints(model.waypoint);
            }

            if (ErrorMessage != null)
            {
                return RedirectToAction("Create", new { ErrorMessage });
            }


            this._waypointDal.CreateNewWaypoint(model.waypoint.Location, model.waypoint.StartDateTime, model.waypoint.EndDateTime, 
                model.waypoint.TripId, model.waypoint.Description);
            return RedirectToAction("../Trips/Details", new { id = LoggedUser.SelectedTrip.Id });
            

        }

        /// <summary>
        /// Redirects to create.
        /// </summary>
        /// <returns></returns>
        public ActionResult RedirectToCreate()
        {

            ViewBag.TripDetails = LoggedUser.SelectedTrip.Name + " " + LoggedUser.SelectedTrip.StartDate + " - " +
                                  LoggedUser.SelectedTrip.EndDate;
            ViewBag.StartDate = LoggedUser.SelectedTrip.StartDate;
            ViewBag.EndDate = LoggedUser.SelectedTrip.EndDate;

            AddedWaypoint waypoint = WaypointsController.verifyTimeViewModel.waypoint;
            return RedirectToAction("Create", waypoint);
        }

        /// <summary>
        /// Redirects to create.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("RedirectToCreate")]
        [ValidateAntiForgeryToken]
        public ActionResult RedirectToCreatePost([Bind(Include = "waypoint")]
            VerifyTimeViewModel model)
        {
            AddedWaypoint waypoint = WaypointsController.verifyTimeViewModel.waypoint;
            return RedirectToAction("Create", waypoint);
        }

        #endregion
    }
}