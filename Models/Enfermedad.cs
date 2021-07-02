using System;

namespace Models
{
    public class Enfermedad
    {
        public Guid EnfermedadId { get; set; }
        public string NombreEnfermedad { get; set; }
        public string DescripcionEnfermedad { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
