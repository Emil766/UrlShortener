using AutoMapper;
using Models.RepositoryModels.ShortUrlRepository;
using Models.ServiceModels.Shortener;

namespace Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ShortUrl, UrlStatisticItem>();
        }
    }
}
