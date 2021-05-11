
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace PersistenceData.Config
{
    public class PaginaConfig
    {
        /// <summary>
        /// establece las restricciones de la tabla
        /// </summary>
        /// <param name="entityTypeBuilder"></param>
        public PaginaConfig(EntityTypeBuilder<Pagina> entityTypeBuilder)
        {
            entityTypeBuilder.Property(p => p.NombrePagina).IsRequired().HasMaxLength(50);
            entityTypeBuilder.Property(p => p.Accion).IsRequired().HasMaxLength(50);
            entityTypeBuilder.Property(p => p.Controlador).IsRequired().HasMaxLength(50);
        }
    }
}
