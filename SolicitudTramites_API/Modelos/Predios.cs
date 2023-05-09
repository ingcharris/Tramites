using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolicitudTramites_API.Modelos
{
    public class Predios
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PredioId { get; set; }

        [Required]
        public int TramiteId { get; set; }

        [ForeignKey("TramiteId")]
        public Tramites Tramites { get; set; }

        [Required]
        [MaxLength(100)]
        public string PredioNombre { get; set; }

        [Required]
        [MaxLength(255)]
        public string PredioDireccion { get; set; }

        [Required]
        [MaxLength(50)]
        public string PredioDepto { get; set; }

        [Required]
        [MaxLength(50)]
        public string PredioCiudad { get; set; }

        [MaxLength(255)]
        public string Username { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaActualizacion { get; set; }
    }
}
