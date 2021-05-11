using MediatR;
using Microsoft.EntityFrameworkCore;
using PersistenceData;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaServicio
{
    public class EliminarServicio
    {
        public class Ejecuta : IRequest<string>
        {
            [Required]
            public int ServicioId { get; set; }
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
                    var estaEnUso = await context.Citas.Where(p => p.ServicioId.Equals(request.ServicioId)).AnyAsync();
                    if (estaEnUso)
                        return "No se puede eliminar este servicio porque esta en uso";
                    var obj = await context.Servicios.Where(p => p.ServicioId.Equals(request.ServicioId)).FirstAsync();
                    context.Servicios.Remove(obj);
                    var rpt = await context.SaveChangesAsync();
                    if (rpt > 0)
                        return "Exito";
                    else
                        return "No se pudo eliminar el servicio";
                }
                catch (Exception e)
                {
                    return "Error: " + e.Message;
                }
            }
        }
    }
}
