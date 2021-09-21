using AutoMapper;
using BurgerRaterApi.Models;
using BurgerRaterApi.Models.Dto.Restaurant;
using BurgerRaterApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BurgerRaterApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [RequiredScope(RequiredScopesConfigurationKey = "AuthSettings:AllowedScope")]
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
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<RestaurantResponseDto>>> GetAllRestaurants()
        {
            var restaurants = await _restaurantService.GetAll();

            var restaurantResponse = _mapper.Map<IEnumerable<RestaurantResponseDto>>(restaurants);

            return restaurantResponse.ToList();
        }

        // GET: api/Restaurants/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<RestaurantResponseDto>> GetRestaurant(Guid id)
        {
            var restaurant = await _restaurantService.GetById(id);

            return _mapper.Map<RestaurantResponseDto>(restaurant);
        }

        // PUT: api/Restaurants/5
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutRestaurant(Guid id, RestaurantUpdateDto updateDto)
        {
            if (id != updateDto.Id)
            {
                return BadRequest();
            }

            if (!(await _restaurantService.Exists(id)))
            {
                return NotFound();
            }

            var restaurant = _mapper.Map<Restaurant>(updateDto);

            await _restaurantService.Update(restaurant);

            return NoContent();
        }

        // POST: api/Restaurants
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<RestaurantResponseDto>> PostRestaurant(RestaurantCreateDto createDto)
        {
            var restaurant = _mapper.Map<Restaurant>(createDto);
            var createdRestaurant = await _restaurantService.Create(restaurant);

            var restaurantResponse = _mapper.Map<RestaurantResponseDto>(createdRestaurant);

            return CreatedAtAction(nameof(PostRestaurant), new { id = restaurantResponse.Id }, restaurantResponse);
        }

        // DELETE: api/Restaurants/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteRestaurant(Guid id)
        {
            await _restaurantService.Delete(id);

            return NoContent();
        }

    }
}
