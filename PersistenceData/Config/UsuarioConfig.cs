using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace PersistenceData.Config
{
    public class UsuarioConfig
    {
        /// <summary>
        /// establece las restricciones de la tabla
        /// </summary>
        /// <param name="entityTypeBuilder"></param>
        public UsuarioConfig(EntityTypeBuilder<Usuario> entityTypeBuilder)
        {
            entityTypeBuilder.Property(p => p.Contra).IsRequired().HasMaxLength(50);
            entityTypeBuilder.Property(p => p.NombreUsuario).IsRequired().HasMaxLength(50);
            entityTypeBuilder.Property(p => p.NombreCompleto).IsRequired().HasMaxLength(100);
            entityTypeBuilder.Property(p => p.Edad).IsRequired();
            entityTypeBuilder.Property(p => p.Direccion).HasMaxLength(100);
        }
    }
}
