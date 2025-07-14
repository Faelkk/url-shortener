using System.ComponentModel.DataAnnotations;

namespace url_shortener.Application.DTOS;

public class CreateShortenerUrlResponseDto()
{
    public required string OriginalUrl { get; set; }

    public required string ShortUrl { get; set; }
}
