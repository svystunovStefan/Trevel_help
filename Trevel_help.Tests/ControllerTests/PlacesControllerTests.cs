using Microsoft.AspNetCore.Mvc;
using Moq;
using Trevel_help.Controllers;
using Trevel_help.Models;
using Trevel_help.Services.Interfaces;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Trevel_help.Tests.ControllerTests
{
    public class PlacesControllerTests
    {
        [Fact]
        public async Task GetByTrip_ReturnsListOfPlaces()
        {
            // Arrange
            var mockService = new Mock<IPlaceService>();
            mockService.Setup(s => s.GetByTripAsync(It.IsAny<int>()))
                       .ReturnsAsync(new List<Place>
                       {
                           new Place { Id = 1, Name = "Place 1", TripId = 1 },
                           new Place { Id = 2, Name = "Place 2", TripId = 1 }
                       });

            var controller = new PlacesController(mockService.Object);

            // Act
            var result = await controller.GetByTrip(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var list = Assert.IsType<List<Place>>(okResult.Value);
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public async Task GetByTrip_ReturnsEmptyList_WhenNoPlaces()
        {
            // Arrange
            var mockService = new Mock<IPlaceService>();
            mockService.Setup(s => s.GetByTripAsync(It.IsAny<int>()))
                       .ReturnsAsync(new List<Place>());  // порожній список

            var controller = new PlacesController(mockService.Object);

            // Act
            var result = await controller.GetByTrip(999);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var list = Assert.IsType<List<Place>>(okResult.Value);
            Assert.Empty(list);
        }

        [Fact]
        public async Task Create_ReturnsCreatedPlace()
        {
            // Arrange
            var mockService = new Mock<IPlaceService>();
            mockService.Setup(s => s.CreateAsync(It.IsAny<Place>()))
                       .ReturnsAsync((Place p) => { p.Id = 1; return p; });

            var controller = new PlacesController(mockService.Object);
            var newPlace = new Place { Name = "New Place", TripId = 1 };

            // Act
            var result = await controller.Create(newPlace);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var place = Assert.IsType<Place>(okResult.Value);
            Assert.Equal(1, place.Id);
            Assert.Equal("New Place", place.Name);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenPlaceExists()
        {
            // Arrange
            var mockService = new Mock<IPlaceService>();
            mockService.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                       .ReturnsAsync(true);

            var controller = new PlacesController(mockService.Object);

            // Act
            var result = await controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenPlaceDoesNotExist()
        {
            // Arrange
            var mockService = new Mock<IPlaceService>();
            mockService.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                       .ReturnsAsync(false);

            var controller = new PlacesController(mockService.Object);

            // Act
            var result = await controller.Delete(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}

