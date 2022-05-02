using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TravelPlannerLibrary;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    /// <summary>
    ///     The travel planner lodgings controller
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class LodgingsController : Controller
    {
        #region Data members

        private static AddedLodging conflictingLodging;
        private static AddedLodging editedConflictingLodging;
        private readonly TravelPlannerDatabaseEntities db = new TravelPlannerDatabaseEntities();

        private readonly LodgingDal _lodgingDal = new LodgingDal();
        private TripDal _tripDal = new TripDal();

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="LodgingsController" /> class.
        /// </summary>
        public LodgingsController()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="LodgingsController" /> class.
        /// </summary>
        /// <param name="tripDal">The trip dal.</param>
        /// <param name="lodgingDal">The lodging dal.</param>
        public LodgingsController(TripDal tripDal, LodgingDal lodgingDal)
        {
            this._lodgingDal = lodgingDal;
            this._tripDal = tripDal;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Details of the lodging point by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     View of lodging details
        /// </returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var lodgingId = (int)id;
            var lodging = this._lodgingDal.GetLodgingById(lodgingId);
            if (lodging == null)
            {
                return HttpNotFound();
            }

            LoggedUser.SelectedLodging = lodging;
            var mapURL = "https://www.google.com/maps/embed/v1/place?key=AIzaSyDJEezkTFgj0PAnzJQJVVEhfZbpUmH27s0";
            var waypointLocation = HttpUtility.UrlEncode(lodging.Location);
            ViewBag.url = mapURL + "&q=" + waypointLocation;
            return View(lodging);
        }

        /// <summary>
        ///     Prompts the user to create a new lodging point
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="ErrorMessage">The error message.</param>
        public ActionResult Create(int? id, string ErrorMessage)
        {
            if (id == null)
            {
                id = LoggedUser.SelectedTrip.Id;
            }

            var tripId = (int)id;
            LoggedUser.SelectedTrip = this._tripDal.GetTripById(tripId);
            ViewBag.TripDetails = LoggedUser.SelectedTrip.Name + " " + LoggedUser.SelectedTrip.StartDate + " - " +
                                  LoggedUser.SelectedTrip.EndDate;
            ViewBag.StartDate = LoggedUser.SelectedTrip.StartDate;
            ViewBag.EndDate = LoggedUser.SelectedTrip.EndDate;
            return View("Create");
        }

        /// <summary>
        ///     Creates the specified lodging point. If the new lodging point conflicts with
        ///     another lodging point, the user will be directed to a view with the conflicting
        ///     points where they must confirm the conflicts.
        /// </summary>
        /// <param name="lodging">The lodging.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Location,StartTime,EndTime,Description")] AddedLodging lodging)
        {
            if (ModelState.IsValid)
            {
                var TripId = LoggedUser.SelectedTrip.Id;
                var ErrorMessage = this.validateDateTimes(lodging);
                if (ErrorMessage == null)
                {
                    ErrorMessage = this.validateConflictingLodgings(lodging);
                }

                if (ErrorMessage != null)
                {
                    conflictingLodging = new AddedLodging {
                        Location = lodging.Location,
                        StartTime = lodging.StartTime,
                        EndTime = lodging.EndTime,
                        TripId = TripId,
                        Description = lodging.Description
                    };
                    return RedirectToAction("CreateWithConflicts", new { ErrorMessage });
                }

                this._lodgingDal.CreateNewLodging(lodging.Location, lodging.StartTime, lodging.EndTime, TripId,
                    lodging.Description);
                return RedirectToAction("../Trips/Details", new { id = LoggedUser.SelectedTrip.Id });
            }

            return View(lodging);
        }

        /// <summary>
        ///     Prompts the user to create the lodging point after displaying conflicting lodging.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="ErrorMessage">The error message.</param>
        public ActionResult CreateWithConflicts(int? id, string ErrorMessage)
        {
            if (id == null)
            {
                id = LoggedUser.SelectedTrip.Id;
            }

            var Lodging = conflictingLodging;
            conflictingLodging = null;
            var tripId = (int)id;
            LoggedUser.SelectedTrip = this._tripDal.GetTripById(tripId);
            ViewBag.TripDetails = LoggedUser.SelectedTrip.Name + " " + LoggedUser.SelectedTrip.StartDate + " - " +
                                  LoggedUser.SelectedTrip.EndDate;
            ViewBag.StartDate = LoggedUser.SelectedTrip.StartDate;
            ViewBag.EndDate = LoggedUser.SelectedTrip.EndDate;
            ViewBag.ErrorMessage = ErrorMessage;
            return View("CreateWithConflicts", Lodging);
        }

        /// <summary>
        ///     Creates the Lodging after displaying conflicting lodging.
        /// </summary>
        /// <param name="lodging">The lodging.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateWithConflicts(
            [Bind(Include = "Location,StartTime,EndTime,Description")]
            AddedLodging lodging)
        {
            if (ModelState.IsValid)
            {
                var TripId = LoggedUser.SelectedTrip.Id;
                this._lodgingDal.CreateNewLodging(lodging.Location, lodging.StartTime, lodging.EndTime, TripId,
                    lodging.Description);
                return RedirectToAction("../Trips/Details", new { id = LoggedUser.SelectedTrip.Id });
            }

            return View(lodging);
        }

        /// <summary>
        ///     Validates the date times for a lodging point.
        /// </summary>
        /// <param name="lodging">The lodging.</param>
        /// <returns>
        ///     ErrorMessage of invalid dates
        /// </returns>
        private string validateDateTimes(AddedLodging lodging)
        {
            string ErrorMessage = null;
            if (lodging.StartTime.CompareTo(lodging.EndTime) > 0)
            {
                ErrorMessage = "The start date must be before the end date";
            }
            return ErrorMessage;
        }

        /// <summary>
        ///     Checks if there are conflicting lodgings for a given lodging point.
        /// </summary>
        /// <param name="lodging">The lodging.</param>
        /// <returns>
        ///     ErrorMessage of the conflicting lodgings
        /// </returns>
        private string validateConflictingLodgings(AddedLodging lodging)
        {
            var overlaps = this._lodgingDal.GetOverlappingLodging(lodging.StartTime, lodging.EndTime);
            string ErrorMessage = null;
            if (overlaps.Count > 0)
            {
                ErrorMessage = "The lodging was not added because of the following conflicts:" + "\n";
                foreach (var overlap in overlaps)
                {
                    ErrorMessage += overlap.Location + " " + overlap.StartTime + " - " + overlap.EndTime + ". ";
                }

                ErrorMessage += "Select \"Confirm With Conflicts\" to add the lodging anyways.";
            }

            return ErrorMessage;
        }

        /// <summary>
        ///     Validates the conflicting lodgings for edited lodging.
        /// </summary>
        /// <param name="lodging">The lodging.</param>
        /// returns>
        ///     String of conflicting lodgings excluding the edited lodging
        /// </returns>
        private string validateConflictingLodgingsForEditedLodging(Lodging lodging)
        {
            var overlaps = this._lodgingDal.GetOverlappingLodgingsForUpdatedLodging(lodging.StartTime, lodging.EndTime, lodging);
            string ErrorMessage = null;
            if (overlaps.Count > 0)
            {
                ErrorMessage = "The lodging was not edited because of the following conflicts:" + "\n";
                foreach (var overlap in overlaps)
                {
                    ErrorMessage += overlap.Location + " " + overlap.StartTime + " - " + overlap.EndTime + ". ";
                }

                ErrorMessage += "Select \"Edit With Conflicts\" to edit the lodging anyways.";
            }

            return ErrorMessage;
        }

        /// <summary>
        ///     Prompts the user to delete the specified lodging.
        /// </summary>
        /// <param name="id">The identifier of the lodging.</param>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var lodgingId = (int)id;
            var lodging = this._lodgingDal.GetLodgingById(lodgingId);
            if (lodging == null)
            {
                return HttpNotFound();
            }

            return View(lodging);
        }

        /// <summary>
        ///     Deletes the confirmed lodging.
        /// </summary>
        /// <param name="id">The identifier of the lodging.</param>
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var lodgingId = id;
            var lodging = this._lodgingDal.GetLodgingById(lodgingId);
            this._lodgingDal.RemoveLodging(lodging);
            return RedirectToAction("../Trips/Details", new { id = LoggedUser.SelectedTrip.Id });
        }

        /// <summary>
        ///     GET: Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="ErrorMessage">The error message.</param>
        /// <returns>
        ///  View of the lodging to be edited
        /// </returns>
        public ActionResult Edit(int? id, string ErrorMessage)
        {
            int lodgingId = (int)id;          
            Lodging lodging = this._lodgingDal.GetLodgingById(lodgingId);
            var TripId = LoggedUser.SelectedTrip.Id;
            LoggedUser.SelectedTrip = this._tripDal.GetTripById(TripId);
            AddedLodging editedLodging = AddedLodging.ConvertLodgingToAddedLodging(lodging);
            ViewBag.TripDetails = LoggedUser.SelectedTrip.Name + " " + LoggedUser.SelectedTrip.StartDate + " - " +
                                  LoggedUser.SelectedTrip.EndDate;
            ViewBag.StartDate = LoggedUser.SelectedTrip.StartDate;
            ViewBag.EndDate = LoggedUser.SelectedTrip.EndDate;
            if (ErrorMessage != null)
            {
                ViewBag.ErrorMessage = ErrorMessage;
            }
            return View(editedLodging);
        }

        /// <summary>
        ///     Edits the specified lodging.
        /// </summary>
        /// <param name="lodging">The lodging.</param>
        /// <returns>
        /// 
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Location,StartTime,EndTime,Description")] AddedLodging lodging)
        {
            if (ModelState.IsValid)
            {
                int tripID = LoggedUser.SelectedTrip.Id;
                Lodging editedLodging = AddedLodging.ConvertAddedLodgingToLodging(lodging);
                var ErrorMessage = this.validateDateTimes(lodging);
                if (ErrorMessage != null)
                {
                    return RedirectToAction("Edit", new { ErrorMessage });
                }
                if (ErrorMessage == null)
                {
                    ErrorMessage = this.validateConflictingLodgingsForEditedLodging(editedLodging);
                }

                if (ErrorMessage != null)
                {
                    editedConflictingLodging = new AddedLodging
                    {
                        Location = lodging.Location,
                        StartTime = lodging.StartTime,
                        EndTime = lodging.EndTime,
                        TripId = tripID,
                        Id = lodging.Id,
                        Description = lodging.Description
                    };
                    return RedirectToAction("EditWithConflicts", new { ErrorMessage });
                }
            
                lodging.TripId = tripID;
                this._lodgingDal.WebUpdateLodging(editedLodging);
                return RedirectToAction("Details", new { id = editedLodging.Id });
            }
            return View(lodging);
        }

        /// <summary>
        ///     GET: Edits the lodging with conflicts.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="ErrorMessage">The error message.</param>
        /// <returns>
        ///     View of edited lodging with error message of conflicts
        /// </returns>
        public ActionResult EditWithConflicts(int? id, string ErrorMessage)
        {
            if (id == null)
            {
                id = LoggedUser.SelectedTrip.Id;
            }

            var Lodging = new AddedLodging()
            {
                Id = editedConflictingLodging.Id,
                TripId = editedConflictingLodging.TripId,
                Location = editedConflictingLodging.Location,
                StartTime = editedConflictingLodging.StartTime,
                EndTime = editedConflictingLodging.EndTime,
                Description = editedConflictingLodging.Description
            };
            var tripId = (int)id;
            LoggedUser.SelectedTrip = this._tripDal.GetTripById(tripId);
            ViewBag.TripDetails = LoggedUser.SelectedTrip.Name + " " + LoggedUser.SelectedTrip.StartDate + " - " +
                                  LoggedUser.SelectedTrip.EndDate;
            ViewBag.StartDate = LoggedUser.SelectedTrip.StartDate;
            ViewBag.EndDate = LoggedUser.SelectedTrip.EndDate;
            ViewBag.ErrorMessage = ErrorMessage;
            return View("EditWithConflicts", Lodging);
        }

        /// <summary>
        ///     POST: Edits the lodging with conflicts.
        /// </summary>
        /// <param name="lodging">The lodging.</param>
        /// <returns>
        ///     View of trip details if successful, lodging edit view otherwise
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditWithConflicts(
            [Bind(Include = "Location,StartTime,EndTime,Description,Id")]
            AddedLodging lodging)
        {
            if (ModelState.IsValid)
            {
                Lodging editedLodging = AddedLodging.ConvertAddedLodgingToLodging(lodging);                
                editedLodging.TripId = LoggedUser.SelectedTrip.Id;

                this._lodgingDal.WebUpdateLodging(editedLodging);
                editedConflictingLodging = null;
                return RedirectToAction("../Trips/Details", new { id = LoggedUser.SelectedTrip.Id });
            }

            return View(lodging);
        }

        #endregion
    }
}