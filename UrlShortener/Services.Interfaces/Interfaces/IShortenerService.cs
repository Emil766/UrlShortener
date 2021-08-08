using Models;
using Models.ServiceModels.Shortener;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IShortenerService
    {
        /// <summary>
        /// Метод сохраняет исходную и соответствующую ей укороченную ссылку и возвращает последнюю
        /// </summary>
        /// <param name="longUrl">Исходная ссылка</param>
        /// <returns>Укороченная строка</returns>
        Task<ServiceResult<string>> GetShortUrlAsync(string longUrl);

        /// <summary>
        /// Метод получения статистики укороченных ссылок
        /// </summary>
        /// <param name="take">Количество требуемых элементов</param>
        /// <param name="lastId">Последний полученный идентификатор (не обязателен)</param>
        /// <returns>Список элементов статистики</returns>
        Task<ServiceResult<IEnumerable<UrlStatisticItem>>> GetUrlsStatisticAsync(int take, string lastId);

        /// <summary>
        /// Метод получения ссылки по идентификатору укороченной
        /// </summary>
        /// <param name="id">Идентфиикатор укороченной ссылки</param>
        /// <returns>Длинная (исходная) ссылка</returns>
        Task<ServiceResult<string>> GetLongUrlByIdAsync(string id);
    }
}
