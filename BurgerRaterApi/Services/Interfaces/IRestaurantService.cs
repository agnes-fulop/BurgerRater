using BurgerRaterApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurgerRaterApi.Services.Interfaces
{
    public interface IRestaurantService
    {
        public Task<IEnumerable<Restaurant>> GetAll();

        public Task<Restaurant> GetById(int id);

        public Task<Restaurant> Create(Restaurant restaurant);

        public Task<Restaurant> Update(Restaurant restaurant);

        public Task Delete(Restaurant restaurant);

        public Task Delete(int id);

        public Task<bool> Exists(int id);
    }
}
