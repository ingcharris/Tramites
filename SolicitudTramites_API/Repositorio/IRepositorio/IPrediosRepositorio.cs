using SolicitudTramites_API.Modelos;

namespace SolicitudTramites_API.Repositorio.IRepositorio
{
    public interface IPrediosRepositorio : IRepositorio<Predios>
    {
        Task<Predios> Actualizar(Predios entidad);
    }
}
