using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    /// <summary>
    ///     The user credentials class
    /// </summary>
    public class UserCredentials
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        /// <value>
        ///     The password.
        /// </value>
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        /// <summary>
        ///     Gets or sets the username.
        /// </summary>
        /// <value>
        ///     The username.
        /// </value>
        [Required]
        public string Username { get; set; }

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public int Id { get; set; }

        #endregion
    }
}