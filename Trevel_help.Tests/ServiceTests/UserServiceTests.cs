using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Trevel_help.Data;
using Trevel_help.Models;
using Trevel_help.Services;
using Xunit;

namespace Trevel_help.Tests.ServiceTests
{
    public class UserServiceTests
    {
        private AppDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsEmptyList()
        {
            // Arrange
            var context = CreateContext();
            var service = new UserService(context);

            // Act
            var users = await service.GetAllAsync();

            // Assert
            Assert.Empty(users);
        }

        [Fact]
        public async Task CreateAsync_AddsUser()
        {
            // Arrange
            var context = CreateContext();
            var service = new UserService(context);

            // Act
            var user = await service.CreateAsync(new User { Name = "John" });

            // Assert
            Assert.NotEqual(0, user.Id);
            Assert.Single(context.Users);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsUser_WhenExists()
        {
            // Arrange
            var context = CreateContext();
            var service = new UserService(context);

            var created = await service.CreateAsync(new User { Name = "Alex" });

            // Act
            var user = await service.GetByIdAsync(created.Id);

            // Assert
            Assert.NotNull(user);
            Assert.Equal("Alex", user!.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
        {
            // Arrange
            var context = CreateContext();
            var service = new UserService(context);

            // Act
            var user = await service.GetByIdAsync(999);

            // Assert
            Assert.Null(user);
        }

        [Fact]
        public async Task DeleteAsync_RemovesUser()
        {
            // Arrange
            var context = CreateContext();
            var service = new UserService(context);

            var user = await service.CreateAsync(new User { Name = "Mike" });

            // Act
            var result = await service.DeleteAsync(user.Id);
            var users = await service.GetAllAsync();

            // Assert
            Assert.True(result);
            Assert.Empty(users);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFalse_WhenUserNotFound()
        {
            // Arrange
            var context = CreateContext();
            var service = new UserService(context);

            // Act
            var result = await service.DeleteAsync(123);

            // Assert
            Assert.False(result);
        }
    }
}

