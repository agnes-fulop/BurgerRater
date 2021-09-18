using AutoMapper;
using BurgerRaterApi.Models;
using BurgerRaterApi.Models.Dto.Review;
using BurgerRaterApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurgerRaterApi.Controllers
{
    [Route("api/Restaurants/{restaurantId}/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<IEnumerable<ReviewResponseDto>>> GetReviewsForRestaurant([FromRoute] int restaurantId)
        {
            var reviews = await _reviewService.GetAllReviewsForRestaurant(restaurantId);

            var reviewResponse = _mapper.Map<IEnumerable<ReviewResponseDto>>(reviews);

            return Ok(reviewResponse);
        }

        // POST: api/Restaurants/1/Reviews
        [HttpPost]
        public async Task<ActionResult<ReviewResponseDto>> PostReview([FromRoute] int restaurantId, [FromBody]ReviewCreateDto reviewDto)
        {
            var review = new Review { RestaurantId = restaurantId };

            var reviewEntity = _mapper.Map(reviewDto, review);
            var createdReview = await _reviewService.Create(reviewEntity);

            var reviewResponse = _mapper.Map<ReviewResponseDto>(createdReview);

            return CreatedAtAction(nameof(PostReview), new { id = createdReview.Id }, reviewResponse);
        }
    }
}
