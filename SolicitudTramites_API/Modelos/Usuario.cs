using System.ComponentModel.DataAnnotations;

namespace SolicitudTramites_API.Modelos
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Username { get; set; }

        [Required]
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

        public string Rol { get; set; }
    }
}
