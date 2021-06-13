using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace PersistenceData.Config
{
    public class BotonConfig
    {
        public BotonConfig(EntityTypeBuilder<Boton> entityTypeBuilder)
        {
            entityTypeBuilder.Property(p => p.NombreBoton).IsRequired().HasMaxLength(20);
            entityTypeBuilder.Property(p => p.Descripcion).IsRequired().HasMaxLength(100);
        }
    }
}
