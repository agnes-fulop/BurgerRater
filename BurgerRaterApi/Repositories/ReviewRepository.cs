using BurgerRaterApi.Data;
using BurgerRaterApi.Models;
using BurgerRaterApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurgerRaterApi.Repositories
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
        }

        public new async Task<IEnumerable<Review>> GetAll()
        {
            return await _context.Reviews.Include(b => b.Restaurant).ToListAsync();
        }
    }
}
