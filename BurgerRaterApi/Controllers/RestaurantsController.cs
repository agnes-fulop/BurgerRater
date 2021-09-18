using AutoMapper;
using BurgerRaterApi.Models;
using BurgerRaterApi.Models.Dto.Restaurant;
using BurgerRaterApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurgerRaterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IMapper _mapper;

        public RestaurantsController(IRestaurantService restaurantService, IMapper mapper)
        {
            _restaurantService = restaurantService;
            _mapper = mapper;
        }

        // GET: api/Restaurants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantResponseDto>>> GetAllRestaurants()
        {
            var restaurants = await _restaurantService.GetAll();

            var restaurantResponse = _mapper.Map<IEnumerable<RestaurantResponseDto>>(restaurants);

            return Ok(restaurantResponse);
        }

        // GET: api/Restaurants/5
        [HttpGet("{id}")]
        public async Task<Restaurant> GetRestaurant(int id)
        {
            return await _restaurantService.GetById(id);
        }

        // PUT: api/Restaurants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(int id, Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return BadRequest();
            }

            if (!(await _restaurantService.Exists(id)))
            {
                return NotFound();
            }

            await _restaurantService.Update(restaurant);

            return NoContent();
        }

        // POST: api/Restaurants
        [HttpPost]
        public async Task<ActionResult<RestaurantResponseDto>> PostRestaurant(RestaurantCreateDto createDto)
        {
            var restaurant = _mapper.Map<Restaurant>(createDto);
            var createdRestaurant = await _restaurantService.Create(restaurant);

            var restaurantResponse = _mapper.Map<RestaurantResponseDto>(createdRestaurant);

            return CreatedAtAction(nameof(PostRestaurant), new { id = restaurantResponse.Id }, restaurantResponse);
        }

        // DELETE: api/Restaurants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            await _restaurantService.Delete(id);

            return NoContent();
        }

    }
}
