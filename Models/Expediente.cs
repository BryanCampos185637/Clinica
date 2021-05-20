using System;

namespace Models
{
    public class Expediente
    {
        public Guid ExpedienteId { get; set; }
        public string CodidoExpediente { get; set; }
        public Guid PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public Int64 EnfermedadId { get; set; }
        public Enfermedad Enfermedad { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
