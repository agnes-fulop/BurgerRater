using BurgerRaterApi.Data;
using BurgerRaterApi.Models;
using BurgerRaterApi.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace BurgerRaterApi.Repositories
{
    public class RestaurantRepository : BaseRepository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Burger> AddBurgerForRestaurant(Guid restaurantId, Burger burger)
        {
            var restaurant = await GetById(restaurantId);

            restaurant?.Burgers.Add(burger);

            await _context.SaveChangesAsync();

            return burger;
        }

        public async Task<Review> AddReviewForRestaurant(Guid restaurantId, Review review)
        {
            var restaurant = await GetById(restaurantId);

            restaurant?.Reviews.Add(review);

            await _context.SaveChangesAsync();

            return review;
        }
    }
}
