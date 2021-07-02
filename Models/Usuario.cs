using System;

namespace Models
{
    public class Usuario
    {
        public Guid UsuarioId { get; set; }
        public string NombreCompleto { get; set; }
        public int Edad { get; set; }
        public string Direccion { get; set; }
        public string NombreUsuario { get; set; }
        public string Contra { get; set; }
        public Guid TipoUsuarioId { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public DateTime FechaCreacion { get; set; }
       
    }
}
