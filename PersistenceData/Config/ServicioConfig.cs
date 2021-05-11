

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace PersistenceData.Config
{
    public class ServicioConfig
    {
        /// <summary>
        /// estable las restricciones de la tabla
        /// </summary>
        /// <param name="entityTypeBuilder"></param>
        public ServicioConfig(EntityTypeBuilder<Servicio> entityTypeBuilder)
        {
            entityTypeBuilder.Property(p => p.DescripcionServicio).HasMaxLength(200).IsRequired();
            entityTypeBuilder.Property(p => p.NombreServicio).HasMaxLength(200).IsRequired();
        }
    }
}
