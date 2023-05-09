using SolicitudTramites_API.Modelos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SolicitudTramites_API.Datos
{
    public class ApplicationDbContext : IdentityDbContext<UsuarioAplicacion>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<UsuarioAplicacion> UsuariosAplicacion { get; set; }
        
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Tramites> Tramites { get; set; }

        public DbSet<Predios> Predios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tramites>().HasData(
                new Tramites()
                {
                    Id = 1,
                    TramiteNombre = "Trámite 1",
                    TramiteDocAdjUrl = "Adjunto para anexar el Documento PDF del trámite...",
                    TramiteDescAdjUrl = "Adjunto para anexar la Descripcion del proyecto en PDF...",
                    TramiteListadoAdj = "Adjunto para anexar en Excel el listado de especies...",
                    Username = "ingcharris@gmail.com",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                },
                new Tramites()
                {
                    Id = 2,
                    TramiteNombre = "Trámite 2",
                    TramiteDocAdjUrl = "Adjunto para anexar el Documento PDF del trámite...",
                    TramiteDescAdjUrl = "Adjunto para anexar la Descripcion del proyecto en PDF...",
                    TramiteListadoAdj = "Adjunto para anexar en Excel el listado de especies...",
                    Username = "ingcharris@gmail.com",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                }
            );
        }

    }
}
