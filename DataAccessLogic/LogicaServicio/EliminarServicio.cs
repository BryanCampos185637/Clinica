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
            public Guid ServicioId { get; set; }
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
                    await context.SaveChangesAsync();                }
                catch (Exception e)
                {
                    return "Error: No se pudo eliminar el servicio, " + e.Message;
                }
                return "Exito";
            }
        }
    }
}
