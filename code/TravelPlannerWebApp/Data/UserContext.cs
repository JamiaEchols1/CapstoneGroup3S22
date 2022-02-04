using Microsoft.EntityFrameworkCore;
using TravelPlannerWebApp.Models;

namespace TravelPlannerWebApp.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
    }
}
