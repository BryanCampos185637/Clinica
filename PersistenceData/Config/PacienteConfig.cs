using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace PersistenceData.Config
{
    public class PacienteConfig
    {
        public PacienteConfig(EntityTypeBuilder<Paciente> entityTypeBuilder)
        {
            entityTypeBuilder.Property(p => p.NombrePaciente).HasMaxLength(200).IsRequired();
            entityTypeBuilder.Property(p => p.ApellidoPaciente).HasMaxLength(200).IsRequired();
            entityTypeBuilder.Property(p => p.NoDuiPaciente).HasMaxLength(10).IsRequired();
            entityTypeBuilder.Property(p => p.EdadPaciente).IsRequired();
        }
    }
}
