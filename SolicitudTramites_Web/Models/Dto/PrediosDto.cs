using System.ComponentModel.DataAnnotations;

namespace SolicitudTramites_Web.Models.Dto
{
    public class PrediosDto
    {
        [Required]
        public int PredioId { get; set; }

        [Required]
        public int TramiteId { get; set; }

        public string PredioNombre { get; set; }

        public string PredioDireccion { get; set; }

        public string PredioDepto { get; set; }

        public string PredioCiudad { get; set; }

        public TramitesDto Tramites { get; set; }

    }
}
