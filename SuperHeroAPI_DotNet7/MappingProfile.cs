using AutoMapper;
using SuperHeroAPI_DotNet7.Models;
using SuperHeroAPI_DotNet7.Models.DTO;

namespace SuperHeroAPI_DotNet7
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SuperHero, SuperHeroDTO>().ReverseMap();
            CreateMap<SuperHero, UpdateSuperHero>().ReverseMap();
        }
    }
}
