using AutoMapper;
using BurgerRaterApi.Controllers;
using BurgerRaterApi.Infrastructure.Exceptions;
using BurgerRaterApi.Mappers;
using BurgerRaterApi.Models;
using BurgerRaterApi.Models.Dto.Restaurant;
using BurgerRaterApi.Services.Interfaces;
using BurgerRaterApi.Tests.Comparers;
using BurgerRaterApi.Tests.TestDataGenerators;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BurgerRaterApi.Tests.ControllerTests
{
    public class RestaurantsControllerTests
    {
        private readonly Mock<IRestaurantService> _restaurantServiceMock;
        private readonly IMapper _mapper;
        private RestaurantsController _controller;

        public RestaurantsControllerTests()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new RestaurantAutoMapperProfile());
            });

            _mapper = mappingConfig.CreateMapper();
            _restaurantServiceMock = new Mock<IRestaurantService>();

            _controller = new RestaurantsController(_restaurantServiceMock.Object, _mapper);
        }

        private static IEnumerable<TestCaseData> GetAllRestaurants_TestCases
        {
            get
            {
                yield return new TestCaseData(new List<Restaurant>()
                {
                    RestaurantGenerator.GenerateRestaurantEntity(),
                    RestaurantGenerator.GenerateRestaurantEntity()
                }, 
                2).SetName("GetAllRestaurants_MultipleResults");
                yield return new TestCaseData(new List<Restaurant>(), 
                0).SetName("GetAllRestaurants_NoResults");
            }
        }

        [Test, TestCaseSource(nameof(GetAllRestaurants_TestCases))]
        public async Task GetAllRestaurants_Successful(List<Restaurant> entities, int expectedCount)
        {
            // Arrange
            _restaurantServiceMock.Setup(service => service.GetAll()).ReturnsAsync(entities);

            // Act
            var result = await _controller.GetAllRestaurants();

            // Assert
            Assert.IsInstanceOf<ActionResult<IEnumerable<RestaurantResponseDto>>>(result);
            Assert.IsAssignableFrom<List<RestaurantResponseDto>>(result.Value);
            Assert.AreEqual(expectedCount, result.Value.Count());
        }

        [Test]
        public async Task GetRestaurant_Successful()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var entity = RestaurantGenerator.GenerateRestaurantEntity(_ => _.Id = testId);

            _restaurantServiceMock.Setup(service => service.GetById(testId)).ReturnsAsync(entity);

            // Act
            var result = await _controller.GetRestaurant(testId);

            // Assert
            Assert.IsInstanceOf<ActionResult<RestaurantResponseDto>>(result);
            Assert.IsAssignableFrom<RestaurantResponseDto>(result.Value);

            var expectedDto = RestaurantGenerator.ConvertEntityToResultDto(entity);
            Assert.That(expectedDto, Is.EqualTo(result.Value).Using(RestaurantResponseDtoComparer.Default));
        }

        [Test]
        public void GetRestaurant_ThrowsNotFoundException_WhenEntityDoesNotExist()
        {
            // Arrange
            var testId = Guid.NewGuid();

            _restaurantServiceMock
                .Setup(service => service.GetById(testId))
                .ThrowsAsync(new NotFoundException("Entity does not exist"));

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() => _controller.GetRestaurant(testId));
        }

        [Test]
        public async Task PostRestaurant_Successful()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var entity = RestaurantGenerator.GenerateRestaurantEntity(_ => _.Id = testId);
            var createDto = RestaurantGenerator.GenerateRestaurantCreateDto();

            _restaurantServiceMock.Setup(service => service.Create(It.IsAny<Restaurant>())).ReturnsAsync(entity);

            // Act
            var result = await _controller.PostRestaurant(createDto);

            // Assert
            Assert.IsAssignableFrom<ActionResult<RestaurantResponseDto>>(result);
            Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);

            var resultValue = ((CreatedAtActionResult)result.Result).Value;
            var expectedDto = RestaurantGenerator.ConvertEntityToResultDto(entity);
            Assert.That(expectedDto, Is.EqualTo(resultValue).Using(RestaurantResponseDtoComparer.Default));
        }

        [Test]
        public void PostRestaurant_ThrowsException()
        {
            // Arrange

            _restaurantServiceMock
                .Setup(service => service.Create(It.IsAny<Restaurant>()))
                .ThrowsAsync(new Exception());

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _controller.PostRestaurant(new RestaurantCreateDto()));
        }

        [Test]
        public async Task PutRestaurant_Successful()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var entity = RestaurantGenerator.GenerateRestaurantEntity(_ => _.Id = testId);
            var updateDto = RestaurantGenerator.GenerateRestaurantUpdateDto(_ => _.Id = testId);

            _restaurantServiceMock.Setup(service => service.Exists(testId)).ReturnsAsync(true);
            _restaurantServiceMock.Setup(service => service.Update(It.IsAny<Restaurant>())).ReturnsAsync(entity);

            // Act
            var result = await _controller.PutRestaurant(testId, updateDto);

            // Assert
            Assert.IsAssignableFrom<NoContentResult>(result);
        }

        [Test]
        public async Task PutRestaurant_ReturnsNotFoundResult_WhenEntityDoesNotExist()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var updateDto = RestaurantGenerator.GenerateRestaurantUpdateDto(_ => _.Id = testId);

            _restaurantServiceMock.Setup(service => service.Exists(testId)).ReturnsAsync(false);

            // Act
            var result = await _controller.PutRestaurant(testId, updateDto);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Test]
        public async Task PutRestaurant_ReturnsBadRequestResult_WhenIdsDoNotMatch()
        {
            // Arrange
            var updateDto = RestaurantGenerator.GenerateRestaurantUpdateDto(_ => _.Id = Guid.NewGuid());

            // Act
            var result = await _controller.PutRestaurant(Guid.NewGuid(), updateDto);

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result);
        }

        [Test]
        public void PutRestaurant_ThrowsException()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var updateDto = RestaurantGenerator.GenerateRestaurantUpdateDto(_ => _.Id = testId);

            _restaurantServiceMock.Setup(service => service.Exists(testId)).ReturnsAsync(true);

            _restaurantServiceMock
                .Setup(service => service.Update(It.IsAny<Restaurant>()))
                .ThrowsAsync(new Exception());

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _controller.PutRestaurant(testId, updateDto));
        }

        [Test]
        public async Task DeleteRestaurant_Successful()
        {
            // Arrange
            var testId = Guid.NewGuid();
            _restaurantServiceMock
                .Setup(service => service.Delete(testId))
                .Verifiable();

            // Act
            var result = await _controller.DeleteRestaurant(testId);

            // Assert
            Assert.IsAssignableFrom<NoContentResult>(result);
            _restaurantServiceMock.Verify(service => service.Delete(testId), Times.Once);
        }

        [Test]
        public void DeleteRestaurant_ThrowsNotFoundException_WhenEntityDoesNotExist()
        {
            // Arrange
            var testId = Guid.NewGuid();

            _restaurantServiceMock
                .Setup(service => service.Delete(testId))
                .ThrowsAsync(new NotFoundException("Entity does not exist"));

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() => _controller.DeleteRestaurant(testId));
        }
    }
}
