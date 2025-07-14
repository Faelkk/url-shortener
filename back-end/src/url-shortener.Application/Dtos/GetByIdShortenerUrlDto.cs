using System.ComponentModel.DataAnnotations;

namespace url_shortener.Application.DTOS;

public class GetByIdShortenerUrlDto
{
    [Required(ErrorMessage = "A url curta é obrigatória")]
    public required string ShortUrl { get; set; }
}
