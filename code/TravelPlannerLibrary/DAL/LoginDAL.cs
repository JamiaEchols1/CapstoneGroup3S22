using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlannerLibrary.Models;

namespace TravelPlannerLibrary.DAL
{
    public class LoginDAL
    {
        private static readonly TravelPlannerDatabaseEntities db = new TravelPlannerDatabaseEntities();

        public static User Login(string username, string password)
        {
            User loggedUser = db.Users.Where(u => u.Username == username).Where(u => u.Password == password).FirstOrDefault<User>();
            return loggedUser;
        }
    }
}
