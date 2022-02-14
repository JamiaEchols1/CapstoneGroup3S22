using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlannerLibrary.Models;
using System.Security.Cryptography;

namespace TravelPlannerLibrary.DAL
{
    public class LoginDAL
    {
        private static TravelPlannerDatabaseEntities db = new TravelPlannerDatabaseEntities();

        public LoginDAL() { }
        public LoginDAL(TravelPlannerDatabaseEntities @object)
        {
            db = @object;
        }

        public User Login(string username, string password)
        {
            var encryptedPassword = Encrypt(password);
            User loggedUser = db.Users.Where(u => u.Username == username).Where(u => u.Password == encryptedPassword).FirstOrDefault<User>();
            return loggedUser;
        }

        public List<User> GetAllUsers()
        {
            var query = from b in db.Users
                        orderby b.Username
                        select b;

            return query.ToList();
        }

        // Credit to https://www.youtube.com/watch?v=EEItNLDw0-A
        public static string Encrypt(String decrypted)
        {
            string hash = "Bbouwmanlmfao@2022$";
            byte[] data = UTF8Encoding.UTF8.GetBytes(decrypted);

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tripDES = new TripleDESCryptoServiceProvider();

            tripDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripDES.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripDES.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(result);
        }

        public static string Decrypt(String encrypted)
        {
            string hash = "Bbouwmanlmfao@2022$";
            byte[] data = Convert.FromBase64String(encrypted);

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tripDES = new TripleDESCryptoServiceProvider();

            tripDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripDES.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripDES.CreateDecryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return UTF8Encoding.UTF8.GetString(result);
        }
    }
}
