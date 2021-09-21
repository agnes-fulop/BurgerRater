using AutoMapper;
using BurgerRaterApi.Models;
using BurgerRaterApi.Models.Dto.Burger;
using BurgerRaterApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurgerRaterApi.Controllers
{
    [Authorize]
    [Route("api/Restaurants/{restaurantId}/[controller]")]
    [ApiController]
    [RequiredScope(RequiredScopesConfigurationKey = "AuthSettings:AllowedScope")]
    public class BurgersController : ControllerBase
    {
        private readonly IBurgerService _burgerService;
        private readonly IMapper _mapper;

        public BurgersController(IBurgerService burgerService, IMapper mapper)
        {
            _burgerService = burgerService;
            _mapper = mapper;
        }

        // GET: api/Restaurants/1/Burgers
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<BurgerResponseDto>>> GetBurgersForRestaurant([FromRoute] Guid restaurantId)
        {
            var burgers = await _burgerService.GetAllBurgersForRestaurant(restaurantId);

            var burgerResponse = _mapper.Map<IEnumerable<BurgerResponseDto>>(burgers);

            return Ok(burgerResponse);
        }

        // POST: api/Restaurants/1/Burgers
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<BurgerResponseDto>> PostBurger([FromRoute] Guid restaurantId, [FromBody] BurgerCreateDto burgerDto)
        {
            var burgerEntity = _mapper.Map<Burger>(burgerDto);
            var createdBurger = await _burgerService.AddBurgerForRestaurant(restaurantId, burgerEntity);

            var burgerResponse = _mapper.Map<BurgerResponseDto>(createdBurger);

            return CreatedAtAction(nameof(PostBurger), new { id = createdBurger.Id }, burgerResponse);
        }
    }
}
