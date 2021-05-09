
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace PersistenceData.Config
{
    public class CitaConfig
    {
        public CitaConfig(EntityTypeBuilder<Cita> entityTypeBuilder)
        {
            entityTypeBuilder.Property(p => p.FechaCita).IsRequired();
        }
    }
}
