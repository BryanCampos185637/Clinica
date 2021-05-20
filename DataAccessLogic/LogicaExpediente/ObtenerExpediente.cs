using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using PersistenceData;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaExpediente
{
    public class ObtenerExpediente
    {
        public class Ejecuta : IRequest<Expediente>
        {
            [Required]
            public Guid ExpedienteId { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, Expediente>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext appDbContext)
            {
                context = appDbContext;
            }
            public async Task<Expediente> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                return await context.Expedientes.Where(p => p.ExpedienteId.Equals(request.ExpedienteId)).FirstOrDefaultAsync();
            }
        }
    }
}
