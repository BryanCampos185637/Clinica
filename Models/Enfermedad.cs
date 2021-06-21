using System;
using System.Collections.Generic;

namespace Models
{
    public class Enfermedad
    {
        public Int64 EnfermedadId { get; set; }
        public string NombreEnfermedad { get; set; }
        public string DescripcionEnfermedad { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
