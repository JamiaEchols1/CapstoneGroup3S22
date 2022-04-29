using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    /// <summary>
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class TransportationController : Controller
    {
        #region Data members

        private readonly TransportationDal _transportationDal = new TransportationDal();
        private readonly WaypointDal _waypointDal = new WaypointDal();

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TransportationController" /> class.
        /// </summary>
        public TransportationController()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TransportationController" /> class.
        /// </summary>
        /// <param name="transportationDal">The transportation dal.</param>
        public TransportationController(TransportationDal transportationDal)
        {
            this._transportationDal = transportationDal;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Details the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var transportationId = (int)id;
            var transportation = this._transportationDal.GetTransportationById(transportationId);
            if (transportation == null)
            {
                return HttpNotFound();
            }

            LoggedUser.SelectedTransportation = transportation;
            return View("Details", transportation);
        }

        /// <summary>
        ///     Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var validatedId = (int)id;
            var transportation = this._transportationDal.GetTransportationById(validatedId);
            if (transportation == null)
            {
                return HttpNotFound();
            }

            return View("Delete", transportation);
        }

        /// <summary>
        ///     Deletes the confirmed.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var transportation = this._transportationDal.GetTransportationById(id);
            this._transportationDal.DeleteTransportation(transportation);
            return RedirectToAction("../Trips/Details", new { id = LoggedUser.SelectedTrip.Id });
        }

        /// <summary>
        ///     GET: Returns a view for a user to create a new transportation for a selected trip
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="ErrorMessage">The error message.</param>
        public ActionResult Create(int? id, string ErrorMessage)
        {
            if (id == null)
            {
                id = LoggedUser.SelectedTrip.Id;
            }

            var transportationId = (int)id;
            LoggedUser.SelectedTrip = this._transportationDal.GetTripFromTransportation(transportationId);
            ViewBag.TripDetails = LoggedUser.SelectedTrip.Name + " " + LoggedUser.SelectedTrip.StartDate + " - " +
                                  LoggedUser.SelectedTrip.EndDate;
            ViewBag.StartDate = LoggedUser.SelectedTrip.StartDate;
            ViewBag.EndDate = LoggedUser.SelectedTrip.EndDate;
            if (ErrorMessage != null)
            {
                ViewBag.ErrorMessage = ErrorMessage;
            }
            AddedTransportation transport = new AddedTransportation();
            transport.Types = new List<string>()
            {
                "WALKING",
                "DRIVING",
                "TRANSIT",
                "BICYCLING"
            };

            return View("Create", transport);
        }

        /// <summary>
        ///     Creates the specified transportation. If there are conflicting transportations,
        ///     an error message with the conflicts will be returned with a new create transportation
        ///     view.
        /// </summary>
        /// <param name="transportation">The transportation.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "StartTime,EndTime,Description,Type,Origin,Destination")]
            AddedTransportation transportation)
        {
            if (ModelState.IsValid)
            {
                transportation.TripId = LoggedUser.SelectedTrip.Id;
                var ErrorMessage = this.validateDateTimes(transportation);
                if (ErrorMessage == null)
                {
                    ErrorMessage = this.validateConflictingTransportAndWaypoints(transportation);
                }

                if (ErrorMessage != null)
                {
                    return RedirectToAction("Create", new { ErrorMessage });
                }

                this._transportationDal.CreateANewTransportation(transportation.TripId, transportation.StartTime,
                    transportation.EndTime, transportation.Description, transportation.Type, transportation.Origin, transportation.Destination);
                return RedirectToAction("../Trips/Details", new { id = LoggedUser.SelectedTrip.Id });
            }

            return View(transportation);
        }

        private string validateDateTimes(AddedTransportation transportation)
        {
            string ErrorMessage = null;
            if (transportation.StartTime.CompareTo(transportation.EndTime) > 0)
            {
                ErrorMessage = "The start date must be before the end date";
            }
            return ErrorMessage;
        }

        private string validateConflictingTransportAndWaypoints(AddedTransportation transportation)
        {
            string ErrorMessage = null;
            var startDate = transportation.StartTime;
            var endDate = transportation.EndTime;
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

        private string validateConflictingTransportAndWaypointsForEditedWaypoint(Transportation transportation)
        {
            string ErrorMessage = null;
            var startDate = transportation.StartTime;
            var endDate = transportation.EndTime;
            var waypointsAndTransportation = new List<object>();
            waypointsAndTransportation.AddRange(this._transportationDal.GetOverlappingTransportationsForUpdatedTransportation(startDate, endDate, transportation));
            waypointsAndTransportation.AddRange(
                this._waypointDal.GetOverlappingWaypoints(startDate, endDate));
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
        ///     Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="ErrorMessage">The error message.</param>
        /// <returns>
        ///     View of transportation to edit
        /// </returns>
        public ActionResult Edit(int? id, string ErrorMessage)
        {
            int transportationId = (int)id;
            Transportation transportation = this._transportationDal.GetTransportationById(transportationId);
            LoggedUser.SelectedTrip = this._transportationDal.GetTripFromTransportation(transportation.TripId);
            AddedTransportation editedTransportation = AddedTransportation.ConvertTransportationToAddedTransportation(transportation);
            ViewBag.TripDetails = LoggedUser.SelectedTrip.Name + " " + LoggedUser.SelectedTrip.StartDate + " - " +
                                  LoggedUser.SelectedTrip.EndDate;
            ViewBag.StartDate = LoggedUser.SelectedTrip.StartDate;
            ViewBag.EndDate = LoggedUser.SelectedTrip.EndDate;
            if (ErrorMessage != null)
            {
                ViewBag.ErrorMessage = ErrorMessage;
            }
            editedTransportation.Types = new List<string>()
            {
                "WALKING",
                "DRIVING",
                "TRANSIT",
                "BICYCLING"
            };
            return View(editedTransportation);
        }

        /// <summary>
        ///     Edits the specified transportation.
        /// </summary>
        /// <param name="transportation">The transportation.</param>
        /// <returns>
        ///     Trip details page if edited successfully, edit view otherwise
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Origin,Destination,StartTime,EndTime,TripId,Description,Type,TravelTime")] AddedTransportation transportation)
        {
            if (ModelState.IsValid)
            {
                Transportation editedTransportation = AddedTransportation.ConvertAddedTransportationToTransportation(transportation);
                var ErrorMessage = this.validateDateTimes(transportation);
                if (ErrorMessage == null)
                {
                    ErrorMessage = this.validateConflictingTransportAndWaypointsForEditedWaypoint(editedTransportation);
                }

                if (ErrorMessage != null)
                {
                    return RedirectToAction("Edit", new { ErrorMessage });
                }
                int tripID = LoggedUser.SelectedTrip.Id;
                transportation.TripId = tripID;
                this._transportationDal.UpdateTransportation(editedTransportation);
                return RedirectToAction("Details", new { id = editedTransportation.Id });
            }
            return View(transportation);
        }

        #endregion
    }
}