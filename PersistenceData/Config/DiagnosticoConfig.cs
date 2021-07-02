using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace PersistenceData.Config
{
    public class DiagnosticoConfig
    {
        public DiagnosticoConfig(EntityTypeBuilder<Diagnostico> entityTypeBuilder)
        {
            entityTypeBuilder.Property(p => p.ExpedienteId);
            entityTypeBuilder.Property(p => p.EnfermedadId);
        }
    }
}
