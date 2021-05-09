using System;

namespace Models
{
    public class TipoUsuario
    {
        public int TipoUsuarioId { get; set; }
        public string NombreTipoUsuario { get; set; }
        public string DescripcionTipoUsuario { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
