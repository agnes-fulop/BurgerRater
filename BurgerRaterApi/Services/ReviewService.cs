using BurgerRaterApi.Models;
using BurgerRaterApi.Repositories.Interfaces;
using BurgerRaterApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BurgerRaterApi.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IRestaurantRepository _restaurantRepository;

        public ReviewService(IReviewRepository reviewRepository, IRestaurantRepository restaurantRepository)
        {
            _reviewRepository = reviewRepository;
            _restaurantRepository = restaurantRepository;
        }

        public async Task<Review> AddReviewForRestaurant(Guid restaurantId, Review review)
        {
            return await _restaurantRepository.AddReviewForRestaurant(restaurantId, review);
        }

        public async Task<IEnumerable<Review>> GetAllReviewsForRestaurant(Guid restaurantId)
        {
            return (await _reviewRepository.GetAll()).Where(r => r.Restaurant.Id == restaurantId);
        }
    }
}
