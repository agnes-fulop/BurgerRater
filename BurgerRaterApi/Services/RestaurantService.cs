using BurgerRaterApi.Models;
using BurgerRaterApi.Repositories.Interfaces;
using BurgerRaterApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurgerRaterApi.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task<Restaurant> Create(Restaurant restaurant)
        {
            return await _restaurantRepository.Create(restaurant);
        }

        public async Task Delete(Restaurant restaurant)
        {
            await _restaurantRepository.Delete(restaurant);
        }

        public async Task Delete(Guid id)
        {
            await _restaurantRepository.Delete(id);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _restaurantRepository.Exists(id);
        }

        public async Task<IEnumerable<Restaurant>> GetAll()
        {
            return await _restaurantRepository.GetAll();
        }

        public async Task<Restaurant> GetById(Guid id)
        {
            return await _restaurantRepository.GetById(id);
        }

        public async Task<Restaurant> Update(Restaurant restaurant)
        {
            return await _restaurantRepository.Update(restaurant);
        }
    }
}
