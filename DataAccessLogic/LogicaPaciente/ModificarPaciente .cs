using MediatR;
using Models;
using PersistenceData;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaPaciente
{
    public class ModificarPaciente
    {
        public class Ejecuta : IRequest<string>
        {
            [Required]
            public Guid PacienteId { get; set; }
            [Display(Name ="NOMBRES")]
            [Required(ErrorMessage ="El nombre es requerido")]
            [StringLength(200,ErrorMessage ="El nombre solo puede contener 200 caracteres")]
            public string NombrePaciente { get; set; }
            [Display(Name = "APELLIDOS")]
            [Required(ErrorMessage = "Los apellidos son requeridos")]
            [StringLength(200, ErrorMessage = "Los apellidos solo pueden contener 200 caracteres")]
            public string ApellidoPaciente { get; set; }
            [Required(ErrorMessage ="La edad es requerida")]
            [Display(Name = "EDAD")]
            [Range(1,150,ErrorMessage ="La edad debe estar entre 1 y 150")]
            public int? EdadPaciente { get; set; }
            [Required(ErrorMessage ="El dui del paciente es requerido")]
            [StringLength(10, ErrorMessage = "El dui debe contener 10 caracteres", MinimumLength = 10)]
            [Display(Name = "DUI")]
            public string NoDuiPaciente { get; set; }
            [Required(ErrorMessage = "La fecha de nacimiento del paciente es requerido")]
            [Display(Name = "FECHA NACIMIENTO")]
            [DataType(DataType.Date)]
            public DateTime? FechaNacimiento { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta,string>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext appDbContext)
            {
                context = appDbContext;
            }
            public async Task<string> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var nveces = context.Pacientes.Where(p => p.NoDuiPaciente.Equals(request.NoDuiPaciente)
                && p.PacienteId != request.PacienteId).Count();
                if (nveces > 0)
                    return "El numero de dui ya existe en el sistema";
                var obj = new Paciente
                {
                    NoDuiPaciente = request.NoDuiPaciente,
                    NombrePaciente = request.NombrePaciente.ToUpper(),
                    ApellidoPaciente = request.ApellidoPaciente.ToUpper(),
                    EdadPaciente = (int)request.EdadPaciente,
                    PacienteId =request.PacienteId,
                    FechaNacimiento = (DateTime)request.FechaNacimiento
                };
                context.Pacientes.Update(obj);
                var rpt = await context.SaveChangesAsync();
                if (rpt > 0)
                    return "Exito";
                else
                   return "No se pudo modificar el paciente";
            }
            
        }
    }
}
