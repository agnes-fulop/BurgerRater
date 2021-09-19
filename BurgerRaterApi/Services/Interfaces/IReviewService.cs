using BurgerRaterApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurgerRaterApi.Services.Interfaces
{
    public interface IReviewService
    {
        public Task<IEnumerable<Review>> GetAllReviewsForRestaurant(int restaurantId);

        public Task<Review> AddReviewForRestaurant(int restaurantId, Review review);
    }
}
