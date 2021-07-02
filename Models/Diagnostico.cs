using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Diagnostico
    {
        [Key]
        public Guid DiagnosticoId { get; set; }
        [ForeignKey("Enfermedad")]
        public Guid EnfermedadId { get; set; }
        public Enfermedad Enfermedad { get; set; }
        [ForeignKey("Expediente")]
        public Guid ExpedienteId { get; set; }
        public Expediente Expediente { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
