
namespace Models
{
    public class FotoUsuario
    {
        public int FotoUsuarioId { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public byte[] Archivo { get; set; }
        public string Extension { get; set; }
    }
}
