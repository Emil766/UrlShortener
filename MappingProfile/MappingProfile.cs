using AutoMapper;
using Models.RepositoryModels.ShortUrlRepository;
using Models.ServiceModels.Shortener;

namespace MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ShortUrl, UrlStatisticItem>();
        }
    }
}
