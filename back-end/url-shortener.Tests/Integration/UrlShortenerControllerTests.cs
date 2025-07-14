using System.Net.Http.Json;
using url_shortener.Application.DTOS;
using url_shortener.Presentation.DTOS;
using url_shortener.Tests;
using Xunit;

namespace url_shortener.Tests.Presentation;

public class UrlShortenerControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public UrlShortenerControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Post_CreatesShortUrl_WhenValid()
    {
        var request = new CreateShortenerUrlRequest { OriginalUrl = "https://google.com" };

        var response = await _client.PostAsJsonAsync("/url-shortener", request);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<CreateShortenerUrlResponseDto>();

        Assert.Equal("https://google.com", result!.OriginalUrl);
        Assert.NotNull(result.ShortUrl);
    }

    [Fact]
    public async Task GetById_ReturnsOriginalUrl_WhenShortUrlExists()
    {
        // Arrange: cria primeiro uma shortUrl
        var request = new CreateShortenerUrlRequest { OriginalUrl = "https://example.com" };

        var postResponse = await _client.PostAsJsonAsync("/url-shortener", request);
        postResponse.EnsureSuccessStatusCode();

        var created = await postResponse.Content.ReadFromJsonAsync<CreateShortenerUrlResponseDto>();
        Assert.NotNull(created);
        Assert.NotNull(created!.ShortUrl);

        // Act: faz o GET com a shortUrl criada
        var getResponse = await _client.GetAsync($"/url-shortener/{created.ShortUrl}");
        getResponse.EnsureSuccessStatusCode();

        var getResult = await getResponse.Content.ReadFromJsonAsync<GetShortenerUrlResponseDto>();

        // Assert
        Assert.NotNull(getResult);
        Assert.Equal(request.OriginalUrl, getResult!.OriginalUrl);
        Assert.Equal(created.ShortUrl, getResult.ShortUrl);
    }
}
