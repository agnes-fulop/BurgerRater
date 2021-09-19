using BurgerRaterApi.Models;
using BurgerRaterApi.Repositories.Interfaces;
using BurgerRaterApi.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BurgerRaterApi.Services
{
    public class BurgerService : IBurgerService
    {
        private readonly IBurgerRepository _burgerRepository;
        private readonly IRestaurantRepository _restaurantRepository;

        public BurgerService(IBurgerRepository burgerRepository, IRestaurantRepository restaurantRepository)
        {
            _burgerRepository = burgerRepository;
            _restaurantRepository = restaurantRepository;
        }

        public async Task<Burger> AddBurgerForRestaurant(int restaurantId, Burger burger)
        {
            return await _restaurantRepository.AddBurgerForRestaurant(restaurantId, burger);
        }

        public async Task<IEnumerable<Burger>> GetAllBurgersForRestaurant(int restaurantId)
        {
            var allBurgers = await _burgerRepository.GetAll();

            return allBurgers.Where(r => r.Restaurant?.Id == restaurantId);
        }
    }
}
