using BurgerRaterApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurgerRaterApi.Services.Interfaces
{
    public interface IBurgerService
    {
        public Task<IEnumerable<Burger>> GetAllBurgersForRestaurant(int restaurantId);

        public Task<Burger> AddBurgerForRestaurant(int restaurantId, Burger burger);
    }
}
