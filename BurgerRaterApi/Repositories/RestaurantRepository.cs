using BurgerRaterApi.Data;
using BurgerRaterApi.Models;
using BurgerRaterApi.Repositories.Interfaces;
using System.Threading.Tasks;

namespace BurgerRaterApi.Repositories
{
    public class RestaurantRepository : BaseRepository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Review> AddReviewForRestaurant(int restaurantId, Review review)
        {
            var restaurant = await _context.Restaurants.FindAsync(restaurantId);

            restaurant?.Reviews.Add(review);

            await _context.SaveChangesAsync();

            return review;
        }
    }
}
