namespace url_shortener.Domain.Entities;

public class UrlShort
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string OriginalUrl { get; set; }

    public required string ShortUrl { get; set; }
}
