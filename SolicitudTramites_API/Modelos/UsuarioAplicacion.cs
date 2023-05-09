using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SolicitudTramites_API.Modelos
{
    public class UsuarioAplicacion :IdentityUser
    {

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
