using System.ComponentModel.DataAnnotations;

public class CreateShortenerUrlDto()
{
    [Required(ErrorMessage = "A url original é obrigatória")]
    public string OriginalUrl { get; set; }

    [Required(ErrorMessage = "A url curta é obrigatória")]
    public string ShortUrl { get; set; }
}
