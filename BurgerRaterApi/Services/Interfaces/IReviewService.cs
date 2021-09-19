using BurgerRaterApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurgerRaterApi.Services.Interfaces
{
    public interface IReviewService
    {
        public Task<IEnumerable<Review>> GetAllReviewsForRestaurant(Guid restaurantId);

        public Task<Review> AddReviewForRestaurant(Guid restaurantId, Review review);
    }
}
