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
    public class ModificarExpediente
    {
        public class Ejecuta : IRequest<string>
        {
            [Required]
            public Guid ExpedienteId { get; set; }
            [Required(ErrorMessage = "El paciente es requerido")]
            [Display(Name = "Paciente")]
            public Guid PacienteId { get; set; }
            [Required(ErrorMessage = "La enfermedad es requerida")]
            [Display(Name = "Enfermedad")]
            public Guid? EnfermedadId { get; set; }
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
                    var existeExpediente = await context.Expedientes.Where(p => p.PacienteId.Equals(request.PacienteId) && p.ExpedienteId != request.ExpedienteId).AnyAsync();
                    if (existeExpediente)
                        return "El expediente ya existe";

                    var obj = await context.Expedientes.Where(p => p.ExpedienteId.Equals(request.ExpedienteId)).FirstAsync();
                    obj.DiagnosticoId = (Guid)request.EnfermedadId;
                    await context.SaveChangesAsync();
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
