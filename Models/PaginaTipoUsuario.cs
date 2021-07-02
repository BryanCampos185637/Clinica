
using System;

namespace Models
{
    public class PaginaTipoUsuario
    {
        public Guid PaginaTipoUsuarioId { get; set; }
        public Guid PaginaId { get; set; }
        public Pagina Pagina { get; set; }
        public Guid TipoUsuarioId { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}
