﻿using System;

namespace Models
{
    public class Cita
    {
        public Guid CitaId { get; set; }
        public Guid ExpedienteId { get; set; }
        public Expediente Expediente { get; set; }
        public int ServicioId { get; set; }
        public Servicio Servicio { get; set; }
        public DateTime FechaCita { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
