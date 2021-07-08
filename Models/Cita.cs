using System;

namespace Models
{
    public class Cita
    {
        public Guid CitaId { get; set; }
        public Guid ExpedienteId { get; set; }
        public Expediente Expediente { get; set; }
        public Guid ServicioId { get; set; }
        public Servicio Servicio { get; set; }
        public string FechaCita { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
