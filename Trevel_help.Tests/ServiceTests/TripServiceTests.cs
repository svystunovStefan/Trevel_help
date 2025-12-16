using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Trevel_help.Data;
using Trevel_help.Models;
using Trevel_help.Services;
using Xunit;

namespace Trevel_help.Tests.ServiceTests
{
    public class TripServiceTests
    {
        private AppDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TripsDb_" + System.Guid.NewGuid())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsEmptyList_WhenNoTrips()
        {
            // Arrange
            var context = GetContext();
            var service = new TripService(context);

            // Act
            var trips = await service.GetAllAsync();

            // Assert
            Assert.Empty(trips);
        }

        [Fact]
        public async Task CreateAsync_AddsTrip()
        {
            // Arrange
            var context = GetContext();
            var service = new TripService(context);
            var trip = new Trip();

            // Act
            var created = await service.CreateAsync(trip);

            // Assert
            Assert.NotNull(created);
            Assert.Equal(1, created.Id);
            Assert.Single(await context.Trips.ToListAsync());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsTrip_WhenExists()
        {
            // Arrange
            var context = GetContext();
            var service = new TripService(context);
            var trip = await service.CreateAsync(new Trip());

            // Act
            var result = await service.GetByIdAsync(trip.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(trip.Id, result!.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
        {
            // Arrange
            var context = GetContext();
            var service = new TripService(context);

            // Act
            var result = await service.GetByIdAsync(999);

            // Assert
            Assert.Null(result);
        }
    }
}
