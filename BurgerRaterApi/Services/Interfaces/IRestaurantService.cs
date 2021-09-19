using BurgerRaterApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurgerRaterApi.Services.Interfaces
{
    public interface IRestaurantService
    {
        public Task<IEnumerable<Restaurant>> GetAll();

        public Task<Restaurant> GetById(Guid id);

        public Task<Restaurant> Create(Restaurant restaurant);

        public Task<Restaurant> Update(Restaurant restaurant);

        public Task Delete(Restaurant restaurant);

        public Task Delete(Guid id);

        public Task<bool> Exists(Guid id);
    }
}
