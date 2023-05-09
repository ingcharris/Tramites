using AutoMapper;
using SolicitudTramites_API.Modelos;
using SolicitudTramites_API.Modelos.Dto;

namespace SolicitudTramites_API
{
    public class MappingConfig : Profile
    {

        public MappingConfig()
        {
            CreateMap<Tramites, TramitesDto>();
            CreateMap<TramitesDto, Tramites>();

            CreateMap<Tramites, TramitesCreateDto>().ReverseMap();
            CreateMap<Tramites, TramitesUpdateDto>().ReverseMap();

            CreateMap<Predios, PrediosDto>().ReverseMap();
            CreateMap<Predios, PrediosCreateDto>().ReverseMap();
            CreateMap<Predios, PrediosUpdateDto>().ReverseMap();

            CreateMap<UsuarioAplicacion, UsuarioDto>().ReverseMap();
        }

    }
}
