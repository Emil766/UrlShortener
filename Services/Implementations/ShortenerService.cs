using AutoMapper;
using Models;
using Models.ServiceModels.Shortener;
using Repository.Interfaces.Interfaces;
using Serilog;
using Services.Configuration;
using Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class ShortenerService : IShortenerService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IShortUrlRepository _shortUrlRepository;
        private readonly ShortenerServiceConfig _shortenerServiceConfig;

        public ShortenerService(
            ILogger logger,
            IMapper mapper,
            IShortUrlRepository shortUrlRepository,
            ShortenerServiceConfig shortenerServiceConfig)
        {
            _logger = logger;
            _mapper = mapper;
            _shortUrlRepository = shortUrlRepository;
            _shortenerServiceConfig = shortenerServiceConfig;
        }

        public async Task<ServiceResult<string>> GetLongUrlByIdAsync(string id)
        {
            var longUrl = await _shortUrlRepository.GetLongUrlByIdAsync(id);
            if (string.IsNullOrEmpty(longUrl))
            {
                return ServiceResult<string>.NotFound();
            }
            await _shortUrlRepository.IncrementRedirectionsCountAsync(id);
            return ServiceResult<string>.Ok(longUrl);
        }

        public async Task<ServiceResult<string>> GetShortUrlAsync(string longUrl)
        {
            var existedShortUrlId = await _shortUrlRepository.ExistAsync(longUrl);
            if (!string.IsNullOrEmpty(existedShortUrlId))
            {
                return ServiceResult<string>.Ok(existedShortUrlId);
            }
            var shortUrlId = await _shortUrlRepository.InsertAsync(longUrl);
            return ServiceResult<string>.Ok(shortUrlId);
        }

        public async Task<ServiceResult<IEnumerable<UrlStatisticItem>>> GetUrlsStatisticAsync(int take, string lastId)
        {
            var historyItems = await _shortUrlRepository.GetManyAsync(take, lastId);
            var resultItems = historyItems.Select(x => _mapper.Map<UrlStatisticItem>(x, opt => opt.AfterMap(
                (s, t) =>
                {
                    t.ShortUrl = $"{_shortenerServiceConfig.SelfBaseUrl}/{t.Id}";
                })));
            return ServiceResult<IEnumerable<UrlStatisticItem>>.Ok(resultItems);
        }
    }
}
