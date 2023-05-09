using SolicitudTramites_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SolicitudTramites_Web.Models.ViewModel
{
    public class PrediosViewModel
    {

        public PrediosViewModel()
        {
            Predios = new PrediosCreateDto();
        }

        public PrediosCreateDto Predios { get; set; }

        public IEnumerable<SelectListItem> TramitesList { get; set; }

    }
}
