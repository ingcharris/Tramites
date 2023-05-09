using SolicitudTramites_Web.Models.Dto;

namespace SolicitudTramites_Web.Services.IServices
{
    public interface ITramitesService
    {
        Task<T> ObtenerTodos<T>(string token);

        Task<T> ObtenerTodosPaginado<T>(string token, int pageNumber = 1, int pageSize = 4);

        Task<T> Obtener<T>(int id, string token);

        Task<T> Crear<T>(TramitesCreateDto dto, string token);

        Task<T> Actualizar<T>(TramitesUpdateDto dto, string token);

        Task<T> Remover<T>(int id, string token); 
    }
}
