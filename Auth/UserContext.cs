using Microsoft.EntityFrameworkCore;
using WellMI.Models;

namespace WellMI.Auth
{
    public class UserContext : DbContext
    {
        public UserContext ( DbContextOptions<UserContext> options )
     : base ( options )
        {
        }
        protected override void OnModelCreating ( ModelBuilder builder )
        {
            base.OnModelCreating ( builder );
        } 
        
        public DbSet<User> user { get; set; }
    }
    
}
