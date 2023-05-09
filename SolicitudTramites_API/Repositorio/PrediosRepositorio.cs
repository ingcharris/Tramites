using SolicitudTramites_API.Datos;
using SolicitudTramites_API.Modelos;
using SolicitudTramites_API.Repositorio.IRepositorio;

namespace SolicitudTramites_API.Repositorio
{
    public class PrediosRepositorio : Repositorio<Predios>, IPrediosRepositorio
    {

        private readonly ApplicationDbContext _db;

        public PrediosRepositorio(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public async Task<Predios> Actualizar(Predios entidad)
        {
           entidad.FechaActualizacion = DateTime.Now;
           _db.Predios.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
