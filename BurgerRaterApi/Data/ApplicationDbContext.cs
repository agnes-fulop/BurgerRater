using BurgerRaterApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BurgerRaterApi.Data
{
    public class ApplicationDbContext
        : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Burger> Burgers { get; set; }
    }
}
