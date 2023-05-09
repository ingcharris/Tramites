using SolicitudTramites_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SolicitudTramites_Web.Models.ViewModel
{
    public class PrediosUpdateViewModel
    {

        public PrediosUpdateViewModel()
        {
            Predios = new PrediosUpdateDto();
        }

        public PrediosUpdateDto Predios { get; set; }
        
        public IEnumerable<SelectListItem> TramitesList { get; set; }

    }
}
