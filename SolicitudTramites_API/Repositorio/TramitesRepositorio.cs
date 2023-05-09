using SolicitudTramites_API.Datos;
using SolicitudTramites_API.Modelos;
using SolicitudTramites_API.Repositorio.IRepositorio;

namespace SolicitudTramites_API.Repositorio
{
    public class TramitesRepositorio : Repositorio<Tramites>, ITramitesRepositorio
    {

        private readonly ApplicationDbContext _db;

        public TramitesRepositorio(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public async Task<Tramites> Actualizar(Tramites entidad)
        {
           entidad.FechaActualizacion = DateTime.Now;
           _db.Tramites.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
