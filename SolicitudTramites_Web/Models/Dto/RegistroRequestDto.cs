namespace SolicitudTramites_Web.Models.Dto
{
    public class RegistroRequestDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Rol { get; set; }

        public string UsuarioNombre { get; set; }

        public int UsuarioTipoDocumento { get; set; }

        public long UsuarioNroDocumento { get; set; }

        public string UsuarioDireccion { get; set; }
    }
}
