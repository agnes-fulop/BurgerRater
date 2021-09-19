using BurgerRaterClient.Models.Dtos;
using BurgerRaterClient.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurgerRaterClient.Pages
{
    public class RestaurantsModel : PageModel
    {
        private readonly IRestaurantService _restaurantService;

        public IEnumerable<RestaurantResponseDto> restaurants = new List<RestaurantResponseDto>();

        public RestaurantsModel(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        public async Task OnGet()
        {
            restaurants = await _restaurantService.GetAllRestaurants();
        }
    }
}
