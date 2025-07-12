using Microsoft.EntityFrameworkCore;
using url_shortener.Domain.Entities;
using url_shortener.Infrastructure.Contexts;

public class DatabaseContext : DbContext, IDatabaseContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options) { }

    public DbSet<UrlShort> UrlShorts { get; set; }

    public override int SaveChanges() => base.SaveChanges();

    public async Task<int> SaveChangesAsync() => await base.SaveChangesAsync();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UrlShort>(entity =>
        {
            entity.ToTable("url_short");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ShortUrl).HasColumnName("short_url");
            entity.Property(e => e.OriginalUrl).HasColumnName("original_url");
        });
    }
}
