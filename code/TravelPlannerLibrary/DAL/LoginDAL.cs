using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TravelPlannerLibrary.Models;

namespace TravelPlannerLibrary.DAL
{
    /// <summary>
    ///     The login Data access layer
    /// </summary>
    public class LoginDal
    {
        #region Data members

        private static TravelPlannerDatabaseEntities db = new TravelPlannerDatabaseEntities();

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="LoginDal" /> class.
        /// </summary>
        public LoginDal()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="LoginDal" /> class.
        /// </summary>
        /// <param name="object">The database entity object.</param>
        public LoginDal(TravelPlannerDatabaseEntities @object)
        {
            db = @object;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Gets the user with the specified credentials if they are valid.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        ///     The user with the specified credentials or default if the credentials are invalid
        /// </returns>
        public User CheckLoginCredentials(string username, string password)
        {
            var encryptedPassword = Encrypt(password);
            var loggedUser = db.Users.Where(u => u.Username == username)
                               .FirstOrDefault(u => u.Password == encryptedPassword);
            return loggedUser;
        }

        /// <summary>
        ///     Gets all users.
        /// </summary>
        /// <returns>
        ///     A collection of all users
        /// </returns>
        public List<User> GetAllUsers()
        {
            var query = from b in db.Users
                        orderby b.Username
                        select b;

            return query.ToList();
        }

        // Credit to https://www.youtube.com/watch?v=EEItNLDw0-A
        /// <summary>
        ///     Encrypts the specified string.
        /// </summary>
        /// <param name="decrypted">The decrypted.</param>
        /// <returns>
        ///     The encrypted string or null
        /// </returns>
        public static string Encrypt(string decrypted)
        {
            const string hash = "Bbouwmanlmfao@2022$";

            if (decrypted == null)
            {
                return null;
            }

            var data = Encoding.UTF8.GetBytes(decrypted);

            var md5 = new MD5CryptoServiceProvider();
            var tripDes = new TripleDESCryptoServiceProvider();

            tripDes.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(hash));
            tripDes.Mode = CipherMode.ECB;

            var transform = tripDes.CreateEncryptor();
            var result = transform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(result);
        }

        #endregion
    }
}