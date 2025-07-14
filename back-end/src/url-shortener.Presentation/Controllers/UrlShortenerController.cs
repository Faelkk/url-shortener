using Microsoft.AspNetCore.Mvc;
using url_shortener.Application.DTOS;
using url_shortener.Application.Services;
using url_shortener.Presentation.DTOS;

namespace url_shortener.Presentation.Controllers;

[ApiController]
[Route("/url-shortener")]
public class UrlShortenerController : Controller
{
    private readonly IUrlShortenerService urlShortenerService;

    public UrlShortenerController(IUrlShortenerService urlShortenerService)
    {
        this.urlShortenerService = urlShortenerService;
    }

    [HttpGet("{shortUrlId}")]
    public async Task<IActionResult> GetById(string shortUrlId)
    {
        var urlShortId = new GetByIdShortenerUrlDto { ShortUrl = shortUrlId };

        var result = await urlShortenerService.GetById(urlShortId);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(
        [FromBody] CreateShortenerUrlRequest createShortenerUrlDto
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var newCreateShortUrl = new CreateShortenerUrlDto
            {
                OriginalUrl = createShortenerUrlDto.OriginalUrl,
            };

            var result = await urlShortenerService.Create(newCreateShortUrl);

            return Ok(result);
        }
        catch (Exception err)
        {
            return BadRequest(new { message = err.Message });
        }
    }
}
