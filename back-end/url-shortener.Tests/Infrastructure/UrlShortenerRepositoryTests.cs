using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using url_shortener.Domain.Entities;
using url_shortener.Infrastructure.Contexts;
using url_shortener.Infrastructure.Repository;
using Xunit;

namespace url_shortener.Tests.Infrastructure
{
    public class UrlShortenerRepositoryTests
    {
        private DatabaseContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            return new DatabaseContext(options);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddUrlShortToDatabase()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repo = new UrlShortenerRepository(context);

            var urlShort = new UrlShort
            {
                OriginalUrl = "https://example.com",
                ShortUrl = "abc12345",
            };

            // Act
            var created = await repo.CreateAsync(urlShort);

            // Assert
            Assert.Equal("abc12345", created.ShortUrl);
            Assert.Single(context.UrlShorts);
        }

        [Fact]
        public async Task GetByShortUrl_ShouldReturnUrlShort_WhenExists()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repo = new UrlShortenerRepository(context);

            var urlShort = new UrlShort
            {
                OriginalUrl = "https://example.com",
                ShortUrl = "abc12345",
            };

            context.UrlShorts.Add(urlShort);
            await context.SaveChangesAsync();

            // Act
            var found = await repo.GetByShortUrl("abc12345");

            // Assert
            Assert.NotNull(found);
            Assert.Equal("https://example.com", found.OriginalUrl);
        }

        [Fact]
        public async Task GetByShortUrl_ShouldReturnNull_WhenNotExists()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repo = new UrlShortenerRepository(context);

            // Act
            var found = await repo.GetByShortUrl("nonexistent");

            // Assert
            Assert.Null(found);
        }
    }
}
