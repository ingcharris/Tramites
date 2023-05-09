using SolicitudTramites_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SolicitudTramites_Web.Models.ViewModel
{
    public class PrediosDeleteViewModel
    {

        public PrediosDeleteViewModel()
        {
            Predios = new PrediosDto();
        }

        public PrediosDto Predios { get; set; }

        public IEnumerable<SelectListItem> TramitesList { get; set; }

    }
}
