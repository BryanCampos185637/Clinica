using System.Collections.Generic;

namespace Models.DTO
{
    public class PacienteDTO: basePaginacion
    {
        public List<Paciente>ListaPacientes { get; set; }
        public PacienteDTO()
        {
                
        }
    }
}
