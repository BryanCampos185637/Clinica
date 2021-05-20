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
    public class AgregarPaciente
    {
        public class Ejecuta : IRequest<string>
        {
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
            [StringLength(10, ErrorMessage ="El dui debe contener 10 caracteres",MinimumLength =10)]
            [Display(Name = "DUI")]
            public string NoDuiPaciente { get; set; }
            [Required(ErrorMessage = "La fecha de nacimiento del paciente es requerido")]
            [Display(Name = "FECHA NACIMIENTO")]
            [DataType(DataType.Date)]
            public DateTime? FechaNacimiento { get; set; }
            [Display(Name = "DIRECCIÓN")]
            [Required(ErrorMessage = "La direccion es requerida")]
            [StringLength(200, ErrorMessage = "El dirección solo puede contener 200 caracteres")]
            public string Direccion { get; set; }
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
                try
                {
                    var nveces = context.Pacientes.Where(p => p.NoDuiPaciente.Equals(request.NoDuiPaciente)).Count();
                    if (nveces > 0)
                        return "El numero de dui ya existe en el sistema";
                    var obj = new Paciente
                    {
                        NoDuiPaciente = request.NoDuiPaciente,
                        NombrePaciente = request.NombrePaciente.ToUpper(),
                        ApellidoPaciente = request.ApellidoPaciente.ToUpper(),
                        EdadPaciente = (int)request.EdadPaciente,
                        FechaCreacion = DateTime.Now,
                        PacienteId = Guid.NewGuid(),
                        Direccion = request.Direccion.ToUpper(),
                        PacienteTieneExpediente = "NO",
                        FechaNacimiento = (DateTime)request.FechaNacimiento
                    };
                    context.Pacientes.Add(obj);
                    var rpt = await context.SaveChangesAsync();
                    if (rpt > 0)
                        return "Exito";
                    else
                        return "No se pudo agregar el paciente";
                }
                catch (Exception e)
                {
                    return "Error "+ e.Message;
                }
            }
            
        }
    }
}
