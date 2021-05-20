using System.Collections.Generic;

namespace Models.DTO
{
    public class ListasParaExpedienteDTO
    {
        public List<Enfermedad>ListaEnfermedad { get; set; }
        public List<Paciente>ListaPaciente { get; set; }
    }
}
