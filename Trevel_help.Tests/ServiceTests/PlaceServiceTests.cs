using Microsoft.EntityFrameworkCore;
using Trevel_help.Data;
using Trevel_help.Models;
using Trevel_help.Services;
using Xunit;

namespace Trevel_help.Tests.ServiceTests
{
    public class PlaceServiceTests
    {
        private AppDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task CreateAsync_AddsPlace()
        {
            var context = GetContext();
            var service = new PlaceService(context);

            await service.CreateAsync(new Place { Name = "Place", TripId = 1 });

            Assert.Single(context.Places);
        }

        [Fact]
        public async Task GetByTripAsync_ReturnsPlaces()
        {
            var context = GetContext();
            context.Places.Add(new Place { Name = "P", TripId = 1 });
            context.SaveChanges();

            var service = new PlaceService(context);

            var result = await service.GetByTripAsync(1);

            Assert.Single(result);
        }

        [Fact]
        public async Task GetByTripAsync_ReturnsEmpty_WhenNoPlaces()
        {
            var service = new PlaceService(GetContext());

            var result = await service.GetByTripAsync(1);

            Assert.Empty(result);
        }

        [Fact]
        public async Task DeleteAsync_RemovesPlace()
        {
            var context = GetContext();
            var place = new Place { Name = "P", TripId = 1 };
            context.Places.Add(place);
            context.SaveChanges();

            var service = new PlaceService(context);

            await service.DeleteAsync(place.Id);

            Assert.Empty(context.Places);
        }
    }
}
