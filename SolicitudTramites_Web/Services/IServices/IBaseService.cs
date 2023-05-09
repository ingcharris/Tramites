using SolicitudTramites_Web.Models;

namespace SolicitudTramites_Web.Services.IServices
{
    public interface IBaseService
    {
        public APIResponse responseModel { get; set; }

        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
