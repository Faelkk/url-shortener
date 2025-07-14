using System.ComponentModel.DataAnnotations;

namespace url_shortener.Application.DTOS;

public class CreateShortenerUrlDto()
{
    [Required(ErrorMessage = "A url original é obrigatória")]
    public required string OriginalUrl { get; set; }
}
