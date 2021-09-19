using BurgerRaterApi.Data;
using BurgerRaterApi.Models;
using BurgerRaterApi.Repositories.Interfaces;

namespace BurgerRaterApi.Repositories
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
