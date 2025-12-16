using Microsoft.EntityFrameworkCore;
using Trevel_help.Data;
using Trevel_help.Models;
using Trevel_help.Services;
using Xunit;

namespace Trevel_help.Tests.ServiceTests
{
    public class UserServiceTests
    {
        private readonly AppDbContext _context;
        private readonly UserService _service;

        public UserServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "ServiceTestDb")
                .Options;
            _context = new AppDbContext(options);
            _service = new UserService(_context);
        }

        [Fact]
        public async Task CreateUser_ShouldAddUser()
        {
            // Arrange
            var user = new User { Username = "Oleg", Email = "oleg@example.com" };

            // Act
            var created = await _service.CreateUserAsync(user);

            // Assert
            Assert.NotNull(created);
            Assert.Equal("Oleg", created.Username);
            Assert.Single(_context.Users);
        }

        [Fact]
        public async Task GetAllUsers_ShouldReturnAllUsers()
        {
            // Arrange
            _context.Users.Add(new User { Username = "Ivan", Email = "ivan@example.com" });
            _context.Users.Add(new User { Username = "Olena", Email = "olena@example.com" });
            await _context.SaveChangesAsync();

            // Act
            var users = await _service.GetAllUsersAsync();

            // Assert
            Assert.Equal(2, users.Count());
        }
    }
}
