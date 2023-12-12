using Microsoft.EntityFrameworkCore;
using WellMI.Models;

namespace WellMI.Data
{
    public class UserContext : DbContext
    {

        public UserContext ( DbContextOptions<UserContext> options ) : base ( options )
        {

        }

        public  DbSet<User> user { get; set; }
    }
}
