using System.Collections.Generic;

namespace Models.DTO
{
    public class RolDTO:basePaginacion
    {
        public List<TipoUsuario>ListaTipoUsuario { get; set; }
    }
}
