using System.ComponentModel.DataAnnotations;

namespace SolicitudTramites_Web.Models.Dto
{
    public class PrediosCreateDto
    {
        [Required]
        public int PredioId { get; set; }

        [Required(ErrorMessage = "El trámite es requerido.")]
        public int TramiteId { get; set; }

        [Required(ErrorMessage = "El nombre del predio es requerido.")]
        [MaxLength(255)]
        public string PredioNombre { get; set; }

        [Required(ErrorMessage = "La dirección del predio es requerida.")]
        [MaxLength(255)]
        public string PredioDireccion { get; set; }

        [Required(ErrorMessage = "El departamento del predio es requerido.")]
        [MaxLength(255)]
        public string PredioDepto { get; set; }

        [Required(ErrorMessage = "La ciudad del predio es requerida.")]
        [MaxLength(255)]
        public string PredioCiudad { get; set; }

        [MaxLength(255)]
        public string Username { get; set; }

    }
}
