namespace url_shortener.Application.DTOS;

public class GetShortenerUrlResponseDto
{
    public required string OriginalUrl { get; set; }
    public required string ShortUrl { get; set; }
}
