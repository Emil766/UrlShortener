using AutoMapper;
using Models;
using Models.ServiceModels.Shortener;
using Repository.Interfaces.Interfaces;
using Services.Interfaces;
using Services.Interfaces.Configuration.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class ShortenerService : IShortenerService
    {
        private readonly IMapper _mapper;
        private readonly IShortUrlRepository _shortUrlRepository;
        private readonly IShortenerServiceConfig _shortenerServiceConfig;

        public ShortenerService(
            IMapper mapper,
            IShortUrlRepository shortUrlRepository,
            IShortenerServiceConfig shortenerServiceConfig)
        {
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
