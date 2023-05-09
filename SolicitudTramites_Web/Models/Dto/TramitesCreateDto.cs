

using System.ComponentModel.DataAnnotations;

namespace SolicitudTramites_Web.Models.Dto
{
    public class TramitesCreateDto
    {
        [Required(ErrorMessage ="El nombre del trámite es requerido.")]
        [MaxLength(255)]
        public string TramiteNombre { get; set; }

        [Required(ErrorMessage = "El documento es requerido.")]
        [MaxLength(4000)]
        public string TramiteDocAdjUrl { get; set; }

        [Required(ErrorMessage = "La descripción es requerida.")]
        [MaxLength(4000)]
        public string TramiteDescAdjUrl { get; set; }

        [MaxLength(4000)]
        public string TramiteListadoAdj { get; set; }

        [MaxLength(255)]
        public string Username { get; set; }
    }
}
