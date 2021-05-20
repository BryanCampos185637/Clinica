
using System.Collections.Generic;

namespace Models.ViewModel
{
    public class DatosExpedienteVM
    {
        public Paciente Paciente { get; set; }
        public Enfermedad Enfermedad { get; set; }
        public List<Servicio>ListaServicios { get; set; }
    }
}
