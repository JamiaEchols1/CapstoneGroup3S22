using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TravelPlannerLibrary;
using TravelPlannerLibrary.Models;

namespace WebApplication4.Controllers
{
    /// <summary>
    ///     The users controller
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class UsersController : Controller
    {
        #region Data members

        private readonly TravelPlannerDatabaseEntities db = new TravelPlannerDatabaseEntities();

        #endregion

        #region Methods

        // GET: Users
        /// <summary>
        ///     Indexes this instance.
        /// </summary>
        /// <returns>
        ///     The user list view result
        /// </returns>
        public ActionResult Index()
        {
            return View(this.db.Users.ToList());
        }

        // GET: Users/Details/5
        /// <summary>
        ///     Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     The user view result
        /// </returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = this.db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <returns>
        ///     The rendered view
        /// </returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        ///     Creates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///     The user view result
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                this.db.Users.Add(user);
                this.db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        /// <summary>
        ///     Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     The user view result
        /// </returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = this.db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        ///     Edits the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///     the index action result
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                this.db.Entry(user).State = EntityState.Modified;
                this.db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Delete/5
        /// <summary>
        ///     Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     The user view result
        /// </returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = this.db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
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
            var user = this.db.Users.Find(id);
            if (user != null)
            {
                this.db.Users.Remove(user);
            }

            this.db.SaveChanges();
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