using Models;
using Models.ServiceModels.Shortener;
using Serilog;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class ShortenerService : IShortenerService
    {
        private readonly ILogger _logger;

        public ShortenerService(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<ServiceResult<string>> GetLongUrlByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<string>> GetShortUrlAsync(string longUrl)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<IEnumerable<UrlStatisticItem>>> GetUrlsStatisticAsync(int take, string lastId)
        {
            throw new NotImplementedException();
        }
    }
}
