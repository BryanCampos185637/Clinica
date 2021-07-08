using MediatR;
using Microsoft.EntityFrameworkCore;
using PersistenceData;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaCita
{
    public class ObtenerCitaPorId
    {
        public class Ejecuta : IRequest<ModificarCita.Ejecuta>
        {
            public Guid CitaId { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, ModificarCita.Ejecuta>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext app)
            {
                context = app;
            }
            public async Task<ModificarCita.Ejecuta> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    var cita = await context.Citas.Where(p => p.CitaId.Equals(request.CitaId)).Include(p=>p.Expediente).Include(p=>p.Servicio).FirstOrDefaultAsync();
                    var paciente = await context.Pacientes.Where(p => p.PacienteId.Equals(cita.Expediente.PacienteId)).FirstAsync();
                    var enfermedad = await context.Enfermedades.Where(p => p.EnfermedadId.Equals(cita.Expediente.DiagnosticoId)).FirstAsync();
                    return new ModificarCita.Ejecuta
                    {
                        CitaId = cita.CitaId,
                        ServicioId = cita.ServicioId,
                        ExpedienteId = cita.ExpedienteId,
                        FechaCita = Convert.ToDateTime(cita.FechaCita),
                        Paciente= paciente,
                        Enfermedad= enfermedad,
                        ListaServicio= await context.Servicios/*.Where(p=>p.ServicioId.Equals(cita.ServicioId))*/.ToListAsync()
                    };
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
