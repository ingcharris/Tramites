using SolicitudTramites_API.Modelos;

namespace SolicitudTramites_API.Repositorio.IRepositorio
{
    public interface ITramitesRepositorio :IRepositorio<Tramites>
    {
        Task<Tramites> Actualizar(Tramites entidad);
    }
}
