using SolicitudTramites_Web.Models.Dto;

namespace SolicitudTramites_Web.Services.IServices
{
    public interface IPrediosService
    {
        Task<T> ObtenerTodos<T>(string token);

        Task<T> Obtener<T>(int id, string token);

        Task<T> Crear<T>(PrediosCreateDto dto, string token);

        Task<T> Actualizar<T>(PrediosUpdateDto dto, string token);

        Task<T> Remover<T>(int id, string token); 
    }
}
