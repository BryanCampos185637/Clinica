using System;

namespace Models
{
    public class Servicio
    {
        public int ServicioId { get; set; }
        public string NombreServicio { get; set; }
        public string DescripcionServicio { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
