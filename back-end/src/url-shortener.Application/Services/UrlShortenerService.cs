using url_shortener.Application.DTOS;
using url_shortener.Application.Services;
using url_shortener.Domain.Entities;

namespace url_shortener.Services;

public class UrlShortenerService : IUrlShortenerService
{
    private readonly IUrlShortenerRepository urlShortenerRepository;

    public UrlShortenerService(IUrlShortenerRepository urlShortenerRepository)
    {
        this.urlShortenerRepository = urlShortenerRepository;
    }

    public async Task<GetShortenerUrlResponseDto> GetById(
        GetByIdShortenerUrlDto getByIdShortenerUrlDto
    )
    {
        var entity = await urlShortenerRepository.GetByShortUrl(getByIdShortenerUrlDto.ShortUrl);

        if (entity is null)
        {
            throw new Exception("URL não encontrada.");
        }

        return new GetShortenerUrlResponseDto
        {
            OriginalUrl = entity.OriginalUrl,
            ShortUrl = entity.ShortUrl,
        };
    }

    public async Task<CreateShortenerUrlResponseDto> Create(
        CreateShortenerUrlDto createShortenerUrlDto
    )
    {
        var url = new UrlShort
        {
            OriginalUrl = createShortenerUrlDto.OriginalUrl,
            ShortUrl = GenerateShortCode(),
        };

        var created = await urlShortenerRepository.CreateAsync(url);

        return new CreateShortenerUrlResponseDto
        {
            OriginalUrl = created.OriginalUrl,
            ShortUrl = created.ShortUrl,
        };
    }

    private string GenerateShortCode()
    {
        var guid = Guid.NewGuid().ToByteArray();
        var base64 = Convert
            .ToBase64String(guid)
            .Replace("+", "")
            .Replace("/", "")
            .Replace("=", "");

        return base64.Substring(0, 8);
    }
}
