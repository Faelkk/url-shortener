using System.ComponentModel.DataAnnotations;

namespace url_shortener.Presentation.DTOS;

public class CreateShortenerUrlRequest
{
    [Required(ErrorMessage = "A url original é obrigatória")]
    public required string OriginalUrl { get; set; }
}
