using BurgerRaterApi.Models;
using BurgerRaterApi.Models.Dto.Restaurant;
using System;

namespace BurgerRaterApi.IntegrationTests.TestDataGenerators
{
    public static class RestaurantGenerator
    {
        public static Restaurant GenerateRestaurantEntity(Action<Restaurant> modifier = null)
        {
            var restaurant = new Restaurant
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Address = "Test street 15",
                City = "Budapest",
                District = "11",
                ZipCode = "1521",
                OpeningTime = "10:00",
                ClosingTime = "22:00"
            };

            modifier?.Invoke(restaurant);

            return restaurant;
        }

        public static RestaurantCreateDto GenerateRestaurantCreateDto(Action<RestaurantCreateDto> modifier = null)
        {
            var restaurant = new RestaurantCreateDto
            {
                Name = "Test",
                Address = "Test street 15",
                City = "Budapest",
                District = "11",
                ZipCode = "1521",
                OpeningTime = "10:00",
                ClosingTime = "22:00"
            };

            modifier?.Invoke(restaurant);

            return restaurant;
        }

        public static RestaurantUpdateDto GenerateRestaurantUpdateDto(Action<RestaurantUpdateDto> modifier = null)
        {
            var restaurant = new RestaurantUpdateDto
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Address = "Test street 15",
                City = "Budapest",
                District = "11",
                ZipCode = "1521",
                OpeningTime = "10:00",
                ClosingTime = "22:00"
            };

            modifier?.Invoke(restaurant);

            return restaurant;
        }
    }
}
