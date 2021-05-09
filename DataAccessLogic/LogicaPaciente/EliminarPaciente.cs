using MediatR;
using Microsoft.EntityFrameworkCore;
using PersistenceData;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaPaciente
{
    public class EliminarPaciente
    {
        public class Ejecuta : IRequest<string>
        {
            public Guid PacienteId { get; set; }
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
                    var obj = await context.Pacientes.Where(p => p.PacienteId.Equals(request.PacienteId)).FirstOrDefaultAsync();
                    context.Pacientes.Remove(obj);
                    var rpt = await context.SaveChangesAsync();
                    if (rpt > 0)
                        return "Exito";
                    else
                        return "No se pudo eliminar el paciente";
                }
                catch (Exception e)
                {
                    return "Error " + e.Message;
                }
            }
        }
    }
}
