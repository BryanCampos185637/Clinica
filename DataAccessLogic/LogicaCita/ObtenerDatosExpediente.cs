using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ViewModel;
using PersistenceData;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaCita
{
    public class ObtenerDatosExpediente
    {
        public class Ejecuta : IRequest<DatosExpedienteVM>
        {
            [Required]
            public Guid ExpedienteId { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, DatosExpedienteVM>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext dbContext)
            {
                context = dbContext;
            }
            public async Task<DatosExpedienteVM> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    DatosExpedienteVM datosExpedienteVM;
                    return datosExpedienteVM = new DatosExpedienteVM
                    {
                        ListaServicios = await context.Servicios.ToListAsync(),
                        Paciente = await context.Expedientes.Include(p => p.Paciente).Where(p => p.ExpedienteId.Equals(request.ExpedienteId))
                            .Select(p => new Paciente {
                                PacienteId = p.PacienteId,
                                NombrePaciente = p.Paciente.NombrePaciente,
                                ApellidoPaciente = p.Paciente.ApellidoPaciente
                            }).FirstOrDefaultAsync(),
                        Enfermedad = await context.Expedientes.Include(p => p.Enfermedad).Where(p => p.ExpedienteId.Equals(request.ExpedienteId))
                            .Select(p => new Enfermedad
                            {
                                EnfermedadId = p.EnfermedadId,
                                NombreEnfermedad = p.Enfermedad.NombreEnfermedad
                            }).FirstOrDefaultAsync()
                    };
                }
                catch (Exception)
                {
                    return null;
                    throw;
                }
            }
        }
    }
}
