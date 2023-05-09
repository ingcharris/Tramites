using SolicitudTramites_Web.Models.Dto;

namespace SolicitudTramites_Web.Services.IServices
{
    public interface IUsuarioService
    {
        Task<T> Login<T>(LoginRequestDto dto);

        Task<T> Registrar<T>(RegistroRequestDto dto);
    }
}
