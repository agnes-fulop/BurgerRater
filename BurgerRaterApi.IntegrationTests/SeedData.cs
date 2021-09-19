using BurgerRaterApi.Data;
using BurgerRaterApi.IntegrationTests.TestDataGenerators;

namespace BurgerRaterApi.IntegrationTests
{
    public static class SeedData
    {
        public static void PopulateTestData(ApplicationDbContext dbContext)
        {
            dbContext.Restaurants.Add(RestaurantGenerator.GenerateRestaurantEntity());
            dbContext.Restaurants.Add(RestaurantGenerator.GenerateRestaurantEntity());
            dbContext.Restaurants.Add(RestaurantGenerator.GenerateRestaurantEntity());

            dbContext.SaveChanges();
        }
    }
}
