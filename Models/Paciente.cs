using System;

namespace Models
{
    public class Paciente
    {
        public Guid PacienteId { get; set; }
        public string NombrePaciente { get; set; }
        public string ApellidoPaciente { get; set; }
        public int EdadPaciente { get; set; }
        public string NoDuiPaciente { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
