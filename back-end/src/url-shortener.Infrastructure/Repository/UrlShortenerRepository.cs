using Microsoft.EntityFrameworkCore;
using url_shortener.Domain.Entities;
using url_shortener.Infrastructure.Contexts;

namespace url_shortener.Infrastructure.Repository;

public class UrlShortenerRepository : IUrlShortenerRepository
{
    private readonly IDatabaseContext databaseContext;

    public UrlShortenerRepository(IDatabaseContext databaseContext)
    {
        this.databaseContext = databaseContext;
    }

    public async Task<UrlShort> CreateAsync(UrlShort url)
    {
        databaseContext.UrlShorts.Add(url);
        await databaseContext.SaveChangesAsync();
        return url;
    }

    public async Task<UrlShort?> GetByShortUrl(string shortUrl)
    {
        return await databaseContext.UrlShorts.FirstOrDefaultAsync(u => u.ShortUrl == shortUrl);
    }
}
