
namespace Models
{
    public class Boton
    {
        public int BotonId { get; set; }
        public string NombreBoton { get; set; }
        public string Descripcion { get; set; }
        public int PaginaId { get; set; }
        public Pagina Pagina { get; set; }
    }
}
