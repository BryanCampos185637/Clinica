using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using PersistenceData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaExpediente
{
    public class AgregarExpediente
    {
        public class Ejecuta : IRequest<string>
        {
            [Required(ErrorMessage ="El paciente es requerido")]
            [Display(Name ="Paciente")]
            public Guid PacienteId { get; set; }
            [Required(ErrorMessage = "La enfermedad es requerida")]
            [Display(Name = "Enfermedad")]
            public Int64? EnfermedadId { get; set; }
            //propiedades para viewModel
            public List<Enfermedad> ListaEnfermedad { get; set; }
            public List<Paciente> ListaPaciente { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, string>
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
                    var existeExpediente = await context.Expedientes.Where(p => p.PacienteId.Equals(request.PacienteId)).AnyAsync();
                    if (existeExpediente)
                        return "El paciente seleccionado ya cuenta con un expediente";

                    #region crear codigo generico
                    var paciente = await context.Pacientes.Where(p => p.PacienteId.Equals(request.PacienteId)).FirstAsync();
                    var arregloApellidos = paciente.ApellidoPaciente.Split(" ");
                    var codigoGenerado = Helper.GenerarCodigo.GenerarCodigoExpediente(paciente);
                    #endregion 

                    context.Expedientes.Add(new Expediente
                    {
                        ExpedienteId = Guid.NewGuid(),
                        EnfermedadId = (int)request.EnfermedadId,
                        PacienteId = request.PacienteId,
                        FechaCreacion = DateTime.UtcNow,
                        CodidoExpediente=codigoGenerado
                    });
                    paciente.PacienteTieneExpediente = "SI";
                    context.Pacientes.Update(paciente);
                    var rpt = await context.SaveChangesAsync();
                    if (rpt <= 0)
                        return "No se pudo crear el expediente";
                }
                catch (Exception e)
                {
                    return "Error" + e.Message;
                }
                return "Exito";
            }
        }
    }
}
