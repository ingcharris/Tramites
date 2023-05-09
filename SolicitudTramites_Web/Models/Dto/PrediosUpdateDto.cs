using System.ComponentModel.DataAnnotations;

namespace SolicitudTramites_Web.Models.Dto
{
    public class PrediosUpdateDto
    {
        [Required]
        public int PredioId { get; set; }

        [Required]
        public int TramiteId { get; set; }

        [Required]
        public string PredioNombre { get; set; }

        [Required]
        public string PredioDireccion { get; set; }

        [Required]
        public string PredioDepto { get; set; }

        [Required]
        public string PredioCiudad { get; set; }

    }
}
