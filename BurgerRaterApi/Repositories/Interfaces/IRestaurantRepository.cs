using BurgerRaterApi.Models;
using System.Threading.Tasks;

namespace BurgerRaterApi.Repositories.Interfaces
{
    public interface IRestaurantRepository : IBaseRepository<Restaurant>
    {
        public Task<Review> AddReviewForRestaurant(int restaurantId, Review review);
    }
}
