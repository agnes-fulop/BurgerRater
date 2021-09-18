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
        private readonly IBaseRepository<Review> _reviewRepository;

        public ReviewService(IBaseRepository<Review> reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<Review> Create(Review review)
        {
            return await _reviewRepository.Create(review);
        }

        public async Task<IEnumerable<Review>> GetAllReviewsForRestaurant(int restaurantId)
        {
            return (await _reviewRepository.GetAll()).Where(r => r.RestaurantId == restaurantId);
        }
    }
}
