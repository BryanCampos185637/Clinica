
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using PersistenceData;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaPaciente
{
    public class ObtenerPaciente
    {
        public class Ejecuta : IRequest<Paciente>
        {
            public Guid PacienteId { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, Paciente>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext appDbContext)
            {
                context = appDbContext;
            }
            public async Task<Paciente> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    return await context.Pacientes.Where(p => p.PacienteId.Equals(request.PacienteId)).FirstOrDefaultAsync();
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
