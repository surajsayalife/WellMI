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

        public  DbSet<EmailHistory> EmailHistory { get; set; }

        public virtual DbSet<Employer> Employer { get; set; }

        public  DbSet<User> User { get; set; }

        protected override void OnConfiguring ( DbContextOptionsBuilder optionsBuilder )
        {
            if ( !optionsBuilder.IsConfigured )
            {
                optionsBuilder.UseSqlServer ( "Server=192.168.29.3;Database=WellMI;User ID=dbuser;Password=test#123;Trusted_Connection=True;TrustServerCertificate=true;MultipleActiveResultSets=true;Encrypt=True" );
            }
        }
    }
    
    


}
