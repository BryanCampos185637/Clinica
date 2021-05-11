
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace PersistenceData.Config
{
    public class CitaConfig
    {
        /// <summary>
        /// pone las restricciones de la tabla
        /// </summary>
        /// <param name="entityTypeBuilder"></param>
        public CitaConfig(EntityTypeBuilder<Cita> entityTypeBuilder)
        {
            entityTypeBuilder.Property(p => p.FechaCita).IsRequired();
        }
    }
}
