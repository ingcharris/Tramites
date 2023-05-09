using SolicitudTramites_Web.Models.Dto;

namespace SolicitudTramites_Web.Models.ViewModel
{
    public class TramitesPaginadoViewModel
    {
        public int PageNumber { get; set; }

        public int TotalPaginas { get; set; }

        public string Previo { get; set; } = "disabled";

        public string Siguiente { get; set; } = "";

        public IEnumerable<TramitesDto> TramitesList { get; set; }
    }
}
