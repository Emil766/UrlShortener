using Models.RepositoryModels.ShortUrlRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces.Interfaces
{
    public interface IShortUrlRepository
    {
        /// <summary>
        /// Метод получения ссылки по идентификатору укороченной
        /// </summary>
        /// <param name="id">Идентфиикатор укороченной ссылки</param>
        /// <returns>Длинная (исходная) ссылка</returns>
        Task<string> GetLongUrlByIdAsync(string id);

        /// <summary>
        /// Метод проверки наличия длинной ссылки в хранилище
        /// </summary>
        /// <param name="longUrl">Длинная (исходная) ссылка</param>
        /// <returns>Идентификатор ссылки</returns>
        Task<string> ExistAsync(string longUrl);

        /// <summary>
        /// Метод добавления новой длинной ссылки
        /// </summary>
        /// <param name="longUrl">Длинная (исходная) ссылка</param>
        /// <returns>Идентификатор ссылки</returns>
        Task<string> InsertAsync(string longUrl);

        /// <summary>
        /// Метод получения множества элементов
        /// </summary>
        /// <param name="take">Количество требуемых элементов</param>
        /// <param name="lastId">Идентификатор последнего полученного элемента</param>
        /// <returns>Список элементов</returns>
        Task<IEnumerable<ShortUrl>> GetManyAsync(int take, string lastId);

        /// <summary>
        /// Метод инкремента количества перенаправлений по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор ссылки</param>
        Task IncrementRedirectionsCountAsync(string id);
    }
}
