using System;
using System.Threading.Tasks;
using Moq;
using url_shortener.Application.DTOS;
using url_shortener.Application.Services;
using url_shortener.Domain.Entities;
using url_shortener.Services;
using Xunit;

namespace url_shortener.Tests.Services
{
    public class UrlShortenerServiceTests
    {
        [Fact]
        public async Task Create_ShouldReturnShortUrl_WhenOriginalUrlIsValid()
        {
            var mockRepo = new Mock<IUrlShortenerRepository>();
            var originalUrl = "https://exemplo.com";

            mockRepo
                .Setup(r => r.CreateAsync(It.IsAny<UrlShort>()))
                .ReturnsAsync((UrlShort u) => u);

            var service = new UrlShortenerService(mockRepo.Object);
            var dto = new CreateShortenerUrlDto { OriginalUrl = originalUrl };

            var result = await service.Create(dto);

            Assert.Equal(originalUrl, result.OriginalUrl);
            Assert.NotNull(result.ShortUrl);
            Assert.Equal(8, result.ShortUrl.Length);
        }

        [Fact]
        public async Task GetById_ShouldReturnOriginalUrl_WhenShortUrlExists()
        {
            var mockRepo = new Mock<IUrlShortenerRepository>();
            var shortUrl = "abc12345";
            var originalUrl = "https://google.com";

            mockRepo
                .Setup(r => r.GetByShortUrl(shortUrl))
                .ReturnsAsync(new UrlShort { OriginalUrl = originalUrl, ShortUrl = shortUrl });

            var service = new UrlShortenerService(mockRepo.Object);
            var dto = new GetByIdShortenerUrlDto { ShortUrl = shortUrl };

            var result = await service.GetById(dto);

            Assert.Equal(originalUrl, result.OriginalUrl);
            Assert.Equal(shortUrl, result.ShortUrl);
        }

        [Fact]
        public async Task GetById_ShouldThrowException_WhenShortUrlDoesNotExist()
        {
            var mockRepo = new Mock<IUrlShortenerRepository>();
            var shortUrl = "inexistente";

            mockRepo.Setup(r => r.GetByShortUrl(shortUrl)).ReturnsAsync((UrlShort?)null);

            var service = new UrlShortenerService(mockRepo.Object);
            var dto = new GetByIdShortenerUrlDto { ShortUrl = shortUrl };

            var exception = await Assert.ThrowsAsync<Exception>(() => service.GetById(dto));
            Assert.Equal("URL não encontrada.", exception.Message);
        }
    }

    public class UrlShortenerService_GenerateShortCodeTests
    {
        [Fact]
        public void GenerateShortCode_ShouldReturn8CharacterString()
        {
            // Arrange
            var service = new UrlShortenerService(null!); // pode passar null se não usar no método

            // Act
            var code = service.GenerateShortCode();

            // Assert
            Assert.NotNull(code);
            Assert.Equal(8, code.Length);
        }

        [Fact]
        public void GenerateShortCode_ShouldOnlyContainValidCharacters()
        {
            var service = new UrlShortenerService(null!);

            var code = service.GenerateShortCode();

            // Base64 sem +,/ e = deve conter letras e números
            Assert.Matches("^[a-zA-Z0-9]{8}$", code);
        }
    }
}
