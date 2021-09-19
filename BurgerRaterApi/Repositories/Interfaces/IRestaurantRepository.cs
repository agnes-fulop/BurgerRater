using BurgerRaterApi.Models;
using System;
using System.Threading.Tasks;

namespace BurgerRaterApi.Repositories.Interfaces
{
    public interface IRestaurantRepository : IBaseRepository<Restaurant>
    {
        public Task<Review> AddReviewForRestaurant(Guid restaurantId, Review review);

        public Task<Burger> AddBurgerForRestaurant(Guid restaurantId, Burger burger);
    }
}
