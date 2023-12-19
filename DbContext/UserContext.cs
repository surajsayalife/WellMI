using Microsoft.EntityFrameworkCore;
using WellMI.Models;

namespace WellMI.Auth
{
    public class UserContext : DbContext
    {
        private string environment;
       

        public UserContext ( DbContextOptions<UserContext> options )
     : base ( options )
        {
        }

        public UserContext ( string environment )
        {
            this.environment = environment;
        }

        protected override void OnModelCreating ( ModelBuilder builder )
        {
            base.OnModelCreating ( builder );
        } 
        
      public DbSet<User> user { get; set; }
        public DbSet<EmailHistory> EmailHistories { get; set; }
    }
    
    


}
