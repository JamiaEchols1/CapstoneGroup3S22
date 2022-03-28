using System.Collections.Generic;

namespace TravelPlannerDesktopApp.Models
{
    /// <summary>
    ///     User class
    /// </summary>
    public class User
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the username.
        /// </summary>
        /// <value>
        ///     The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        /// <value>
        ///     The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        ///     Gets or sets the trips.
        /// </summary>
        /// <value>
        ///     The trips.
        /// </value>
        public virtual ICollection<Trip> Trips { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="User" /> class.
        /// </summary>
        public User()
        {
            this.Trips = new HashSet<Trip>();
        }

        #endregion
    }
}