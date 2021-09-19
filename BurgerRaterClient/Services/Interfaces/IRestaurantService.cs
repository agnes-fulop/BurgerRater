using BurgerRaterClient.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurgerRaterClient.Services.Interfaces
{
    public interface IRestaurantService
    {
        public Task<IEnumerable<RestaurantResponseDto>> GetAllRestaurants();
    }
}
