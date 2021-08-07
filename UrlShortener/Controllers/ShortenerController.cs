using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using UrlShortener.Requests.Shortener;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShortenerController : CustomControllerBase
    {
        private readonly IShortenerService _shortenerService;

        public ShortenerController(IShortenerService shortenerService)
        {
            _shortenerService = shortenerService;
        }

        [Route("/{shortUrlId}")]
        [HttpGet]
        public async Task<IActionResult> RedirectAsync([Required, FromRoute] string shortUrlId)
        {
            var longUrl = await _shortenerService.GetLongUrlByIdAsync(shortUrlId);
            if (longUrl.IsSuccessResult)
            {
                return Redirect(longUrl.Model);
            }
            return StatusCode(longUrl);
        }

        [HttpPost("GetShortUrl")]
        public async Task<IActionResult> GetShortUrlAsync([Required, FromBody] string longUrl)
        {
            var result = await _shortenerService.GetShortUrlAsync(longUrl);
            return StatusCode(result);
        }

        [HttpPost("GetUrlsStatistic")]
        public async Task<IActionResult> GetUrlsStatisticAsync([Required, FromBody] GetUrlsStatisticRequest request)
        {
            var result = await _shortenerService.GetUrlsStatisticAsync(request.Take, request.LastId);
            return StatusCode(result);
        }
    }
}
