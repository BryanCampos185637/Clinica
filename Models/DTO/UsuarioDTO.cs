using System.Collections.Generic;

namespace Models.DTO
{
    public class UsuarioDTO:basePaginacion
    {
        public List<Usuario> ListaUsuario { get; set; }
    }
}
