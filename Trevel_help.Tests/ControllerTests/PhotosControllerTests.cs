using Microsoft.AspNetCore.Mvc;
using Moq;
using Trevel_help.Controllers;
using Trevel_help.Models;
using Trevel_help.Services.Interfaces;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Trevel_help.Tests.ControllerTests;

public class PhotosControllerTests
{
    [Fact]
    public async Task Get_ReturnsOkWithPhotos()
    {
        var mockService = new Mock<IPhotoService>();
        mockService.Setup(s => s.GetByTripAsync(It.IsAny<int>()))
            .ReturnsAsync(new List<Photo> { new Photo { Id = 1, Url = "img.jpg", PlaceId = 1 } });

        var controller = new PhotosController(mockService.Object);

        var result = await controller.Get(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var photos = Assert.IsType<List<Photo>>(okResult.Value);
        Assert.Single(photos);
    }

    [Fact]
    public async Task Create_ReturnsOkWithPhoto()
    {
        var mockService = new Mock<IPhotoService>();
        mockService.Setup(s => s.CreateAsync(It.IsAny<Photo>()))
            .ReturnsAsync((Photo p) => { p.Id = 1; return p; });

        var controller = new PhotosController(mockService.Object);
        var newPhoto = new Photo { Url = "img.jpg", PlaceId = 1 };

        var result = await controller.Create(1, newPhoto);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPhoto = Assert.IsType<Photo>(okResult.Value);
        Assert.Equal(1, returnedPhoto.Id);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenPhotoExists()
    {
        var mockService = new Mock<IPhotoService>();
        mockService.Setup(s => s.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);

        var controller = new PhotosController(mockService.Object);

        var result = await controller.Delete(1);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenPhotoDoesNotExist()
    {
        var mockService = new Mock<IPhotoService>();
        mockService.Setup(s => s.DeleteAsync(It.IsAny<int>())).ReturnsAsync(false);

        var controller = new PhotosController(mockService.Object);

        var result = await controller.Delete(999);

        Assert.IsType<NotFoundResult>(result);
    }
}
