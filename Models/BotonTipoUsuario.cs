
namespace Models
{
    public class BotonTipoUsuario
    {
        public int BotonTipoUsuarioId { get; set; }
        public int BotonId { get; set; }
        public Boton Boton { get; set; }
        public int TipoUsuarioId { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}
