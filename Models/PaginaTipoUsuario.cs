
namespace Models
{
    public class PaginaTipoUsuario
    {
        public int PaginaTipoUsuarioId { get; set; }
        public int PaginaId { get; set; }
        public Pagina Pagina { get; set; }
        public int TipoUsuarioId { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}
