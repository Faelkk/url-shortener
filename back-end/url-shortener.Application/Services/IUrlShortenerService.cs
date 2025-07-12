using url_shortener.Application.DTOS;

namespace url_shortener.Application.Services;

public interface IUrlShortenerService
{
    Task<CreateShortenerUrlResponseDto> Create(CreateShortenerUrlDto dto);
    Task<GetShortenerUrlResponseDto> GetById(GetByIdShortenerUrlDto dto);
}
