using System.ComponentModel.DataAnnotations;

namespace SolicitudTramites_Web.Models.Dto
{
    public class UsuarioDto
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "El correo electrónico es Requerido")]
        [MaxLength(255)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El Password es Requerido")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El Nombre del Solicitante es Requerido")]
        [MaxLength(255)]
        public string UsuarioNombre { get; set; }

        [Required(ErrorMessage = "El Tipo de Documento del Solicitante es Requerido")]
        [MaxLength(255)]
        public int UsuarioTipoDocumento { get; set; }

        [Required(ErrorMessage = "El Número de Documento del Solicitante es Requerido")]
        [MaxLength(255)]
        public long UsuarioNroDocumento { get; set; }

        [Required(ErrorMessage = "La Dirección del Solicitante es Requerida")]
        [MaxLength(255)]
        public string UsuarioDireccion { get; set; }
    }
}
