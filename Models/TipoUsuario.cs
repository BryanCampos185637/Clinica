using System;

namespace Models
{
    public class TipoUsuario
    {
        public Guid TipoUsuarioId { get; set; }
        public string NombreTipoUsuario { get; set; }
        public string DescripcionTipoUsuario { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
