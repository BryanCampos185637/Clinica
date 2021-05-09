using MediatR;
using Microsoft.EntityFrameworkCore;
using PersistenceData;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaEnfermedad
{
    public class EliminarEnfermedad
    {
        public class Ejecta : IRequest<string>
        {
            public Int64 EnfermedadId { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecta, string>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext _context)
            {
                context = _context;
            }
            public async Task<string> Handle(Ejecta request, CancellationToken cancellationToken)
            {
                try
                {
                    var obj = await context.Enfermedades.Where(p => p.EnfermedadId.Equals(request.EnfermedadId)).FirstOrDefaultAsync();
                    context.Enfermedades.Remove(obj);
                    var rpt = await context.SaveChangesAsync();
                    if (rpt > 0)
                        return "Exito";
                    else
                        return "No se pudo eliminar la enfermedad";
                }
                catch (Exception e)
                {
                    return "Error " + e.Message;
                }
            }
        }
    }
}
