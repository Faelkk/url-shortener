using url_shortener.Domain.Entities;

public interface IUrlShortenerRepository
{
    Task<UrlShort> CreateAsync(UrlShort url);
    Task<UrlShort?> GetByShortUrl(string shortUrl);
}
