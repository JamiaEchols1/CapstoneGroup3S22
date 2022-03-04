using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelPlannerLibrary.Models;
using System.Security.Cryptography;

namespace TravelPlannerLibrary.DAL
{
    public class LoginDAL
    {
        private static TravelPlannerDatabaseEntities _db = new TravelPlannerDatabaseEntities();

        public LoginDAL() { }
        public LoginDAL(TravelPlannerDatabaseEntities @object)
        {
            _db = @object;
        }

        public User Login(string username, string password)
        {
            string encryptedPassword = Encrypt(password);
            User loggedUser = _db.Users.Where(u => u.Username == username).FirstOrDefault(u => u.Password == encryptedPassword);
            return loggedUser;
        }

        public List<User> GetAllUsers()
        {
            var query = from b in _db.Users
                        orderby b.Username
                        select b;

            return query.ToList();
        }

        // Credit to https://www.youtube.com/watch?v=EEItNLDw0-A
        public static string Encrypt(String decrypted)
        {
            string hash = "Bbouwmanlmfao@2022$";

            if (decrypted == null)
            {
                return null;
            }

            byte[] data = UTF8Encoding.UTF8.GetBytes(decrypted);

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tripDES = new TripleDESCryptoServiceProvider();

            tripDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripDES.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripDES.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(result);
        }
    }
}
