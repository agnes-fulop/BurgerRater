using BurgerRaterApi.Data;
using BurgerRaterApi.Models;
using BurgerRaterApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurgerRaterApi.Repositories
{
    public class BurgerRepository : BaseRepository<Burger>, IBurgerRepository
    {
        public BurgerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public new async Task<IEnumerable<Burger>> GetAll()
        {
            return await _context.Burgers.Include(b => b.Restaurant).ToListAsync();
        }
    }
}
