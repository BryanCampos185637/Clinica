using MediatR;
using Microsoft.EntityFrameworkCore;
using PersistenceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaCita
{
    public class EliminarCita
    {
        public class Ejecuta : IRequest<string> 
        {
            public Guid CitaId { get; set; }
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
                    var cita = await context.Citas.Where(p => p.CitaId.Equals(request.CitaId)).FirstOrDefaultAsync();
                    if (cita == null)
                        return "No se encontro ninguna cita que coindica";
                    context.Citas.Remove(cita);
                    var rpt = await context.SaveChangesAsync();
                    if (rpt <= 0)
                        return "No se pudo eliminar la cita";
                }
                catch (Exception e)
                {
                    return "Error " + e.Message;
                }
                return "Exito";
            }
        }
    }
}
