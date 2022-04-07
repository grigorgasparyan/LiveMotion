using AutoMapper;

namespace LiveMotion.WPFCliet.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.Slide, Core.Entities.Slide>().ReverseMap();
            CreateMap<Models.Presentation, Core.Entities.Presentation>().ReverseMap();
        }      
    }
}
