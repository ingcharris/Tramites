using AutoMapper;
using SolicitudTramites_Web.Models.Dto;

namespace SolicitudTramites_Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<TramitesDto, TramitesCreateDto>().ReverseMap();
            CreateMap<TramitesDto, TramitesUpdateDto>().ReverseMap();

            CreateMap<PrediosDto, PrediosCreateDto>().ReverseMap();
            CreateMap<PrediosDto, PrediosUpdateDto>().ReverseMap();
        }
    }
}
