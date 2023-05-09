using SolicitudTramites_Utilidad;
using SolicitudTramites_Web.Models;
using SolicitudTramites_Web.Models.Dto;
using SolicitudTramites_Web.Services.IServices;

namespace SolicitudTramites_Web.Services
{
    public class PrediosService : BaseService, IPrediosService
    {
        public readonly IHttpClientFactory _httpClient;

        private string _tramiteUrl;

        public PrediosService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _httpClient = httpClient;
            _tramiteUrl = configuration.GetValue<string>("ServiceUrls:API_URL");
        }

        public Task<T> Actualizar<T>(PrediosUpdateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.PUT,
                Datos = dto,
                Url = _tramiteUrl + "/api/v1/Predios/"+dto.PredioId,
                Token = token
            });
        }

        public Task<T> Crear<T>(PrediosCreateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.POST,
                Datos = dto,
                Url= _tramiteUrl+ "/api/v1/Predios",
                Token = token
            });
        }

        public Task<T> Obtener<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                Url = _tramiteUrl + "/api/v1/Predios/" + id,
                Token = token
            });
        }

        public Task<T> ObtenerTodos<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                Url = _tramiteUrl + "/api/v1/Predios",
                Token = token
            });
        }

        public Task<T> Remover<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.DELETE,
                Url = _tramiteUrl + "/api/v1/Predios/" + id,
                Token = token
            });
        }
    }
}
