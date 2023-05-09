using System.ComponentModel.DataAnnotations;

namespace SolicitudTramites_API.Modelos.Dto
{
    public class PrediosUpdateDto
    {
        [Required]
        public int PredioId { get; set; }

        [Required]
        public int TramiteId { get; set; }

        [Required]
        public string PredioNombre { get; set; }

        public string PredioDireccion { get; set; }

        public string PredioDepto { get; set; }

        public string PredioCiudad { get; set; }

    }
}
