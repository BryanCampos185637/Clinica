using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
namespace PersistenceData.Config
{
    public class ExpedienteConfig
    {
        public ExpedienteConfig(EntityTypeBuilder<Expediente> entityTypeBuilder)
        {
            entityTypeBuilder.Property(p => p.CodidoExpediente).HasMaxLength(15);
        }
    }
}
