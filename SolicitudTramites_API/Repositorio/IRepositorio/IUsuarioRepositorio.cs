using SolicitudTramites_API.Modelos;
using SolicitudTramites_API.Modelos.Dto;

namespace SolicitudTramites_API.Repositorio.IRepositorio
{
    public interface IUsuarioRepositorio
    {
        bool IsUsuarioUnico(string userName);

        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);

        Task<UsuarioDto> Registrar(RegistroRequestDTO registroRequestDTO);
    }
}
