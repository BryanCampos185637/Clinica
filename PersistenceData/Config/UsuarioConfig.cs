using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace PersistenceData.Config
{
    public class UsuarioConfig
    {
        public UsuarioConfig(EntityTypeBuilder<Usuario> entityTypeBuilder)
        {
            entityTypeBuilder.Property(p => p.Contra).IsRequired().HasMaxLength(50);
            entityTypeBuilder.Property(p => p.NombreUsuario).IsRequired().HasMaxLength(50);
        }
    }
}
