
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace PersistenceData.Config
{
    public class EnfermedadConfig
    {
        public EnfermedadConfig(EntityTypeBuilder<Enfermedad> entityTypeBuilder)
        {
            entityTypeBuilder.Property(p => p.DescripcionEnfermedad).IsRequired().HasMaxLength(200);
            entityTypeBuilder.Property(p => p.NombreEnfermedad).IsRequired().HasMaxLength(200);
        }
    }
}
