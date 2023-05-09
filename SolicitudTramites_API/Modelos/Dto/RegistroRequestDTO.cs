namespace SolicitudTramites_API.Modelos.Dto
{
    public class RegistroRequestDTO
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
