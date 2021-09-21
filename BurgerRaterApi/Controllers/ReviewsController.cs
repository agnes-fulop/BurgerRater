using AutoMapper;
using BurgerRaterApi.Models;
using BurgerRaterApi.Models.Dto.Review;
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
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public ReviewsController(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        // GET: api/Restaurants/1/Reviews
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<ReviewResponseDto>>> GetReviewsForRestaurant([FromRoute] Guid restaurantId)
        {
            var reviews = await _reviewService.GetAllReviewsForRestaurant(restaurantId);

            var reviewResponse = _mapper.Map<IEnumerable<ReviewResponseDto>>(reviews);

            return Ok(reviewResponse);
        }

        // POST: api/Restaurants/1/Reviews
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ReviewResponseDto>> PostReview([FromRoute] Guid restaurantId, [FromBody]ReviewCreateDto reviewDto)
        {
            var reviewEntity = _mapper.Map<Review>(reviewDto);
            var createdReview = await _reviewService.AddReviewForRestaurant(restaurantId, reviewEntity);

            var reviewResponse = _mapper.Map<ReviewResponseDto>(createdReview);

            return CreatedAtAction(nameof(PostReview), new { id = createdReview.Id }, reviewResponse);
        }
    }
}
