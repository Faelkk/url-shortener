using Microsoft.EntityFrameworkCore;
using url_shortener.Domain.Entities;

namespace url_shortener.Infrastructure.Contexts;

public interface IDatabaseContext
{
    DbSet<UrlShort> UrlShorts { get; }

    Task<int> SaveChangesAsync();
}
