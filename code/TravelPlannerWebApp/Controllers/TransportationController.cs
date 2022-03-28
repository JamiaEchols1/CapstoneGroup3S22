using System;
using System.Collections.Generic;
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
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class TransportationController : Controller
    {
        private readonly TransportationDal _transportationDal = new TransportationDal();

        /// <summary>
        /// Initializes a new instance of the <see cref="TransportationController"/> class.
        /// </summary>
        public TransportationController()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransportationController"/> class.
        /// </summary>
        /// <param name="transportationDal">The transportation dal.</param>
        public TransportationController(TransportationDal transportationDal)
        {
            _transportationDal = transportationDal;
        }


        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var transportation = this._transportationDal.GetTransportationsByTrip(LoggedUser.SelectedTrip.Id);
            ViewBag.TripName = LoggedUser.SelectedTrip.Name;
            return View("Index", transportation);
        }

        /// <summary>
        /// Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int transportationId = (int)id;
            Transportation transportation = _transportationDal.GetTransportationById(transportationId);
            if (transportation == null)
            {
                return HttpNotFound();
            }
            LoggedUser.SelectedTransportation = transportation;
            return View("Details", transportation);
        }


        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int validatedId = (int)id;
            Transportation transportation = _transportationDal.GetTransportationById(validatedId);
            if (transportation == null)
            {
                return HttpNotFound();
            }
            return View("Delete", transportation);
        }


        /// <summary>
        /// Deletes the confirmed.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transportation transportation = _transportationDal.GetTransportationById(id);
            this._transportationDal.DeleteTransportation(transportation);
            return RedirectToAction("Index");
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
            LoggedUser.SelectedTrip = _transportationDal.GetTripFromTransportation(transportationId);
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
        /// Creates the specified transportation.
        /// </summary>
        /// <param name="transportation">The transportation.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StartTime,EndTime,Description")] AddedTransportation transportation)
        {
            if (ModelState.IsValid)
            {
                transportation.TripId = LoggedUser.SelectedTrip.Id;
                string ErrorMessage = validateDateTimes(transportation);
                if (ErrorMessage == null)
                {
                    ErrorMessage = validateConflictingTransportation(transportation);
                }
                if (ErrorMessage != null)
                {
                    return RedirectToAction("Create", new { ErrorMessage = ErrorMessage });
                }
                else
                {
                    this._transportationDal.CreateANewTransportation(transportation.TripId, transportation.StartTime,
                        transportation.EndTime, transportation.Description);
                    return RedirectToAction("Index");
                }
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
            if (transportation.EndTime.CompareTo(transportation.StartTime) < 0)
            {
                ErrorMessage = "The end date must be after the start date";
            }
            return ErrorMessage;
        }

        private string validateConflictingTransportation(AddedTransportation transportation)
        {
            var overlaps =
                this._transportationDal.GetOverlappingTransportation(transportation.StartTime, transportation.EndTime);
            string ErrorMessage = null;
            if (overlaps.Count > 0)
            {
                ErrorMessage = "The transportation was not added because of the following conflicts:" + "\n";
                foreach (var overlap in overlaps)
                {
                    ErrorMessage += overlap.Description + " " + overlap.StartTime + " - " + overlap.EndTime + ". ";
                }
            }
            return ErrorMessage;
        }
    }
}