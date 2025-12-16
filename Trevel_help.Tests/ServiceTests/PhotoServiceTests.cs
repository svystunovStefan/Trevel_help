using Microsoft.EntityFrameworkCore;
using Trevel_help.Data;
using Trevel_help.Models;
using Trevel_help.Services;
using Xunit;
using Trevel_help.Services;
using Trevel_help.Data;
using Trevel_help.Models;
using Microsoft.EntityFrameworkCore;

namespace Trevel_help.Tests.ServiceTests
{
    public class PhotoServiceTests
    {
        private AppDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task CreateAsync_AddsPhoto()
        {
            var context = GetContext();
            var service = new PhotoService(context);

            await service.CreateAsync(new Photo { Url = "img", Id = 1 });

            Assert.Single(context.Photos);
        }

        [Fact]
        public async Task DeleteAsync_RemovesPhoto()
        {
            var context = GetContext();
            var photo = new Photo { Url = "img", Id = 1 };
            context.Photos.Add(photo);
            context.SaveChanges();

            var service = new PhotoService(context);

            await service.DeleteAsync(photo.Id);

            Assert.Empty(context.Photos);
        }

        [Fact]
        public async Task DeleteAsync_DoesNothing_WhenNotFound()
        {
            var service = new PhotoService(GetContext());

            await service.DeleteAsync(999);

            Assert.True(true);
        }
    }
}
