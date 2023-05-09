using SolicitudTramites_Utilidad;
using SolicitudTramites_Web.Models;
using SolicitudTramites_Web.Models.Dto;
using SolicitudTramites_Web.Services.IServices;

namespace SolicitudTramites_Web.Services
{
    public class TramitesService : BaseService, ITramitesService
    {
        public readonly IHttpClientFactory _httpClient;

        private string _tramiteUrl;

        public TramitesService(IHttpClientFactory httpClient, IConfiguration configuration) :base(httpClient)
        {
            _httpClient = httpClient;
            _tramiteUrl = configuration.GetValue<string>("ServiceUrls:API_URL");
        }

        public Task<T> Actualizar<T>(TramitesUpdateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.PUT,
                Datos = dto,
                Url = _tramiteUrl + "/api/v1/Tramite/" + dto.Id,
                Token = token
            });
        }

        public Task<T> Crear<T>(TramitesCreateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.POST,
                Datos = dto,
                Url= _tramiteUrl+ "/api/v1/Tramite",
                Token = token
            });
        }

        public Task<T> Obtener<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                Url = _tramiteUrl + "/api/v1/Tramite/" + id,
                Token = token
            });
        }

        public Task<T> ObtenerTodos<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                Url = _tramiteUrl + "/api/v1/Tramite",
                Token = token
            });
        }

        public Task<T> ObtenerTodosPaginado<T>(string token, int pageNumber =1, int pageSize =4)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                Url = _tramiteUrl + "/api/v1/Tramite/TramitesPaginado",
                Token = token,
                Parametros = new Parametros() { PageNumber = pageNumber , PageSize = pageSize}
            });
        }


        public Task<T> Remover<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.DELETE,
                Url = _tramiteUrl + "/api/v1/Tramite/" + id,
                Token = token
            });
        }
    }
}
