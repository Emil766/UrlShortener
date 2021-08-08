using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Requests.Shortener;
using Services.Interfaces;
using UrlShortener.Requests.Shortener;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShortenerController : BaseController
    {
        private readonly IShortenerService _shortenerService;

        public ShortenerController(IShortenerService shortenerService)
        {
            _shortenerService = shortenerService;
        }

        /// <summary>
        /// Метод перенаправления по короткой ссылке на исходную
        /// </summary>
        /// <param name="shortUrlId">Идентификатор ссылки</param>
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

        /// <summary>
        /// Метод создания короткой ссылки
        /// </summary>
        /// <param name="request">Исходная - длинная ссылка</param>
        /// <returns>Короткая ссылка</returns>
        [HttpPost("AddShortUrl")]
        public async Task<IActionResult> AddShortUrlAsync([Required, FromBody] AddShortUrlRequest request)
        {
            var result = await _shortenerService.GetShortUrlAsync(request.LongUrl);
            return StatusCode(result);
        }

        /// <summary>
        /// Метод получения статистики по перенаправления по ссылкам
        /// </summary>
        /// <param name="request">Запрос с пагинацией</param>
        /// <returns>Список ссылок с количеством переходов по ним</returns>
        [HttpPost("GetUrlsStatistic")]
        public async Task<IActionResult> GetUrlsStatisticAsync([Required, FromBody] GetUrlsStatisticRequest request)
        {
            var result = await _shortenerService.GetUrlsStatisticAsync(request.Take, request.LastId);
            return StatusCode(result);
        }
    }
}
