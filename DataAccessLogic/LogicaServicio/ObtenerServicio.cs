using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using PersistenceData;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaServicio
{
    public class ObtenerServicio
    {
        public class Ejecuta : IRequest<Servicio>
        {
            [Required]
            public Guid ServicioId { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, Servicio>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext appDbContext)
            {
                context = appDbContext;
            }

            public async Task<Servicio> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    return await context.Servicios.Where(p => p.ServicioId.Equals(request.ServicioId)).FirstOrDefaultAsync();
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
    }
}
