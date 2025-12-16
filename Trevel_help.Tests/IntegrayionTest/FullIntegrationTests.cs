using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Trevel_help.Data;
using Trevel_help.Models;
using Trevel_help.Services;
using Xunit;

namespace Trevel_help.Tests.IntegrationTests
{
    public class FullIntegrationTests
    {
        private AppDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task FullFlow_CreateTripPlacePhoto_WorksCorrectly()
        {
            // Arrange
            var context = GetContext();

            var tripService = new TripService(context);
            var placeService = new PlaceService(context);
            var photoService = new PhotoService(context);

            // Act 1 — Create Trip (БЕЗ Name / Date)
            var trip = await tripService.CreateAsync(new Trip());

            // Act 2 — Create Place
            var place = await placeService.CreateAsync(new Place
            {
                TripId = trip.Id
            });

            // Act 3 — Create Photo
            var photo = await photoService.CreateAsync(new Photo
            {
                Id = trip.Id,
                Url = "photo.jpg"
            });

            // Assert
            Assert.Single(await context.Trips.ToListAsync());
            Assert.Single(await context.Places.ToListAsync());
            Assert.Single(await context.Photos.ToListAsync());

            Assert.Equal(trip.Id, place.Id);
            Assert.Equal(trip.Id, photo.Id);
        }
    }
}
