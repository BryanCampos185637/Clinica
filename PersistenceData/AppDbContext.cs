using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using PersistenceData.Config;

namespace PersistenceData
{
    public class AppDbContext: IdentityDbContext
    {
        /// <summary>
        /// Inyeccion de la conexion a la base de datos
        /// </summary>
        /// <param name="options"></param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        /// <summary>
        /// Me permite poner restricciones a las propiedades en la base de datos
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new CitaConfig(modelBuilder.Entity<Cita>());
            new EnfermedadConfig(modelBuilder.Entity<Enfermedad>());
            new PacienteConfig(modelBuilder.Entity<Paciente>());
            new PaginaConfig(modelBuilder.Entity<Pagina>());
            new ServicioConfig(modelBuilder.Entity<Servicio>());
            new UsuarioConfig(modelBuilder.Entity<Usuario>());
            new ExpedienteConfig(modelBuilder.Entity<Expediente>());
        }
        
        public DbSet<Cita>Citas { get; set; }
        public DbSet<Enfermedad>Enfermedades { get; set; }
        public DbSet<Paciente>Pacientes { get; set; }
        public DbSet<Pagina>Paginas { get; set; }
        public DbSet<PaginaTipoUsuario>paginaTipoUsuarios { get; set; }
        public DbSet<Servicio>Servicios { get; set; }
        public DbSet<TipoUsuario>TipoUsuarios { get; set; }
        public DbSet<Usuario>Usuarios { get; set; }
        public DbSet<Expediente>Expedientes { get; set; }
    }
}
