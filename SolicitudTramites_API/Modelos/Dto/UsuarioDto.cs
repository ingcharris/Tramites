using System.ComponentModel.DataAnnotations;

namespace SolicitudTramites_API.Modelos.Dto
{
    public class UsuarioDto
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string UserName { get; set; }

        public string Password { get; set; }

        [Required]
        [MaxLength(255)]
        public string UsuarioNombre { get; set; }

        [Required]
        [MaxLength(50)]
        public int UsuarioTipoDocumento { get; set; }

        [Required]
        [MaxLength(50)]
        public long UsuarioNroDocumento { get; set; }

        [MaxLength(255)]
        public string UsuarioDireccion { get; set; }
    }
}
