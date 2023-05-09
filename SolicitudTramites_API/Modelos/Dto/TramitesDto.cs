

using System.ComponentModel.DataAnnotations;

namespace SolicitudTramites_API.Modelos.Dto
{
    public class TramitesDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string TramiteNombre { get; set; }

        public string TramiteDocAdjUrl { get; set; }

        public string TramiteDescAdjUrl { get; set; }

        public string TramiteListadoAdj { get; set; }

        public string Username { get; set; }
    }
}
