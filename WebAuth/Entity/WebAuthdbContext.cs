using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace WebAuth.Entity;

public class WebAuthdbContext : DbContext
{
    public WebAuthdbContext(DbContextOptions<WebAuthdbContext> options) : base(options)
        {
        }
        // Registered DB Model in OurHeroDbContext file
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Setting a primary key in OurHero model
            modelBuilder.Entity<Users>().HasKey(x => x.User_Id);

            // Inserting record in OurHero table
            modelBuilder.Entity<Users>().HasData(
                new Users
                {
                    User_Id = Guid.NewGuid(),
                    FirstName = "Anand",
                    LastName = "Shah",
                    UserName = "anandshah"
                    
                }
            );
        }


}
