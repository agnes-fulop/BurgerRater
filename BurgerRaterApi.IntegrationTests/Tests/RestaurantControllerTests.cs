using BurgerRaterApi.Data;
using BurgerRaterApi.IntegrationTests.TestDataGenerators;
using BurgerRaterApi.Models;
using BurgerRaterApi.Models.Dto.Restaurant;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace BurgerRaterApi.IntegrationTests.Tests
{
    [TestFixture]
    public class RestaurantControllerTests
    {
        private CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void GivenARequestToTheController()
        {
            _factory = new CustomWebApplicationFactory<Startup>();
            _client = _factory.CreateClient();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        private static IEnumerable<TestCaseData> CreateRestaurant_BadRequests
        {
            get
            {
                yield return new TestCaseData(RestaurantGenerator.GenerateRestaurantCreateDto(_ => _.Name = null));
                yield return new TestCaseData(RestaurantGenerator.GenerateRestaurantCreateDto(_ => _.Name = string.Empty));
                yield return new TestCaseData(RestaurantGenerator.GenerateRestaurantCreateDto(_ => _.Name = new string('A', 101)));
                yield return new TestCaseData(RestaurantGenerator.GenerateRestaurantCreateDto(_ => _.Address = null));
                yield return new TestCaseData(RestaurantGenerator.GenerateRestaurantCreateDto(_ => _.Address = string.Empty));
                yield return new TestCaseData(RestaurantGenerator.GenerateRestaurantCreateDto(_ => _.Address = new string('A', 201)));
                yield return new TestCaseData(RestaurantGenerator.GenerateRestaurantCreateDto(_ => _.District = null));
                yield return new TestCaseData(RestaurantGenerator.GenerateRestaurantCreateDto(_ => _.District = string.Empty));
                yield return new TestCaseData(RestaurantGenerator.GenerateRestaurantCreateDto(_ => _.District = new string('A', 16)));
                yield return new TestCaseData(RestaurantGenerator.GenerateRestaurantCreateDto(_ => _.ZipCode = null));
                yield return new TestCaseData(RestaurantGenerator.GenerateRestaurantCreateDto(_ => _.ZipCode = string.Empty));
                yield return new TestCaseData(RestaurantGenerator.GenerateRestaurantCreateDto(_ => _.ZipCode = new string('A', 16)));
                yield return new TestCaseData(RestaurantGenerator.GenerateRestaurantCreateDto(_ => _.OpeningTime = null));
                yield return new TestCaseData(RestaurantGenerator.GenerateRestaurantCreateDto(_ => _.OpeningTime = string.Empty));
                yield return new TestCaseData(RestaurantGenerator.GenerateRestaurantCreateDto(_ => _.OpeningTime = "9AM"));
                yield return new TestCaseData(RestaurantGenerator.GenerateRestaurantCreateDto(_ => _.ClosingTime = null));
                yield return new TestCaseData(RestaurantGenerator.GenerateRestaurantCreateDto(_ => _.ClosingTime = string.Empty));
                yield return new TestCaseData(RestaurantGenerator.GenerateRestaurantCreateDto(_ => _.ClosingTime = "wrong"));
            }
        }

        private static IEnumerable<TestCaseData> GetAllRestaurants_TestCases
        {
            get
            {
                yield return new TestCaseData(
                    new List<Restaurant>(), 
                    0);
                yield return new TestCaseData(
                    new List<Restaurant> { RestaurantGenerator.GenerateRestaurantEntity() }, 
                    1);
                yield return new TestCaseData(
                    new List<Restaurant> { RestaurantGenerator.GenerateRestaurantEntity(), RestaurantGenerator.GenerateRestaurantEntity() },
                    2);
            }
        }

        [Test, TestCaseSource(nameof(GetAllRestaurants_TestCases))]
        public async Task GetRestaurants_Successful(List<Restaurant> existingRestaurants, int expectedCount)
        {
            using (var scope = _factory.Services.CreateScope())
            {
                // Arrange 
                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureDeleted();

                foreach (var restaurant in existingRestaurants)
                {
                    context.Restaurants.Add(restaurant);
                }
                context.SaveChanges();

                // Act
                var httpResponse = await _client.GetAsync("/api/Restaurants");

                // Assert
                httpResponse.EnsureSuccessStatusCode();

                var stringResponse = await httpResponse.Content.ReadAsStringAsync();
                var restaurants = JsonConvert.DeserializeObject<IEnumerable<RestaurantResponseDto>>(stringResponse);

                Assert.AreEqual(expectedCount, restaurants.Count());
            }
        }

        [Test]
        public async Task PostRestaurant_Successful()
        {
            using (var scope = _factory.Services.CreateScope()) 
            {
                // Arrange 
                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureDeleted();

                var createDto = RestaurantGenerator.GenerateRestaurantCreateDto();
                var stringContent = new StringContent(JsonConvert.SerializeObject(createDto), Encoding.UTF8, "application/json");

                // Act
                var httpResponse = await _client.PostAsync("/api/Restaurants", stringContent);

                // Assert
                httpResponse.EnsureSuccessStatusCode();

                var stringResponse = await httpResponse.Content.ReadAsStringAsync();
                var restaurants = JsonConvert.DeserializeObject<RestaurantResponseDto>(stringResponse);

                Assert.That(context.Restaurants.Count() == 1);
            }
        }

        [Test, TestCaseSource(nameof(CreateRestaurant_BadRequests))]
        public async Task PostRestaurant_ReturnBadRequestResult(RestaurantCreateDto createDto)
        {
            using (var scope = _factory.Services.CreateScope())
            {
                // Arrange 
                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureDeleted();

                var stringContent = new StringContent(JsonConvert.SerializeObject(createDto), Encoding.UTF8, "application/json");

                // Act
                var httpResponse = await _client.PostAsync("/api/Restaurants", stringContent);

                // Assert
                Assert.False(httpResponse.IsSuccessStatusCode);
                Assert.AreEqual(HttpStatusCode.BadRequest, httpResponse.StatusCode);
                Assert.IsEmpty(context.Restaurants);
            }
        }
    }
}
