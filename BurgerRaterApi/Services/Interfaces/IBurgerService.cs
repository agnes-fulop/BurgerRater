using BurgerRaterApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurgerRaterApi.Services.Interfaces
{
    public interface IBurgerService
    {
        public Task<IEnumerable<Burger>> GetAllBurgersForRestaurant(Guid restaurantId);

        public Task<Burger> AddBurgerForRestaurant(Guid restaurantId, Burger burger);
    }
}
