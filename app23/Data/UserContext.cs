using app23.Models;
using Microsoft.EntityFrameworkCore;

namespace app23.Data
{
    public class UserContext : DbContext 
    {
        public DbSet<User> Users { get; set; }

        //konstruktor
        public UserContext(DbContextOptions options) : base(options) { }
    
        //konfigracja modelu
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(x => x.Name).IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.Surname).IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.Age).IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.Email).IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.IsActive).IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.IsActive).HasDefaultValue("false");
        }
    }
}
