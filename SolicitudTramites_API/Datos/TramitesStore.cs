using SolicitudTramites_API.Modelos.Dto;

namespace SolicitudTramites_API.Datos
{
    public static class TramitesStore
    {
        public static List<TramitesDto> tramitesList = new List<TramitesDto>
        {
            new TramitesDto{ Id=1, TramiteNombre="Trámite 1", TramiteDocAdjUrl="", TramiteDescAdjUrl="", TramiteListadoAdj="", Username="ingcharris@gmail.com" },
            new TramitesDto{ Id=2, TramiteNombre="Trámite 2", TramiteDocAdjUrl="", TramiteDescAdjUrl="", TramiteListadoAdj="", Username="ingcharris@gmail.com" }
        };
    }
}
