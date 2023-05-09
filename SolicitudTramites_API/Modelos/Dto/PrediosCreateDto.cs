using System.ComponentModel.DataAnnotations;

namespace SolicitudTramites_API.Modelos.Dto
{
    public class PrediosCreateDto
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
