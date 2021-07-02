using MediatR;
using Microsoft.EntityFrameworkCore;
using PersistenceData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaExpediente
{
    public class EliminarExpediente
    {
        public class Ejecuta : IRequest<string>
        {
            [Required]
            public Guid ExpedienteId { get; set; }
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
                using(var transaccionSQL = context.Database.BeginTransaction())
                {
                    try
                    {
                        var estaEnUso = await context.Citas.Where(p => p.ExpedienteId.Equals(request.ExpedienteId)).AnyAsync();
                        if (estaEnUso)
                            return "No se puede eliminar el expediente porque se encuentra en uso";
                        var ListaDiagnostico = await context.Diagnosticos.Where(p => p.ExpedienteId.Equals(request.ExpedienteId)).ToListAsync();
                        foreach(var diagnostico in ListaDiagnostico)
                        {
                            context.Diagnosticos.Remove(diagnostico);
                        }
                        var obj = await context.Expedientes.Where(p => p.ExpedienteId.Equals(request.ExpedienteId)).FirstOrDefaultAsync();
                        if (obj == null)
                            return "No se encontro ningun expediente que coincidiera";
                        var paciente = await context.Pacientes.Where(p => p.PacienteId.Equals(obj.PacienteId)).FirstAsync();
                        context.Expedientes.Remove(obj);
                        paciente.PacienteTieneExpediente = "NO";
                        context.Pacientes.Update(paciente);

                        await context.SaveChangesAsync();
                        transaccionSQL.Commit();
                    }
                    catch (Exception e)
                    {
                        transaccionSQL.Rollback();
                        return "Error " + e.Message;
                    }
                }
                return "Exito";
            }
        }
    }
}
