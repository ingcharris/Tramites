using SolicitudTramites_Utilidad;
using SolicitudTramites_Web.Models;
using SolicitudTramites_Web.Models.Dto;
using SolicitudTramites_Web.Services.IServices;

namespace SolicitudTramites_Web.Services
{
    public class UsuarioService : BaseService, IUsuarioService
    {
        public readonly IHttpClientFactory _httpClient;

        private string _usuarioUrl;

        public UsuarioService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _httpClient = httpClient;
            _usuarioUrl = configuration.GetValue<string>("ServiceUrls:API_URL");
        }


        public Task<T> Login<T>(LoginRequestDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.POST,
                Datos = dto,
                Url = _usuarioUrl + "/api/v1/usuario/login"
            });
        }

        public Task<T> Registrar<T>(RegistroRequestDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.POST,
                Datos = dto,
                Url = _usuarioUrl + "/api/v1/usuario/registrar"
            });
        }
    }
}
