using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolicitudTramites_API.Modelos
{
    public class Tramites
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string TramiteNombre { get; set; }

        [Required]
        [MaxLength(4000)]
        public string TramiteDocAdjUrl { get; set; }

        [MaxLength(4000)]
        public string TramiteDescAdjUrl { get; set; }

        [MaxLength(4000)]
        public string TramiteListadoAdj { get; set; }

        [MaxLength(255)]
        public string Username { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaActualizacion { get; set; }
    }
}
