using MediatR;
using PersistenceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaCita
{
    public class ModificarCita
    {
        public class Ejecuta : IRequest<string>
        {
            public Guid CitaId { get; set; }
            public Guid ExpedienteId { get; set; }
            public int ServicioId { get; set; }
            public DateTime FechaCita { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, string>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext dbContext)
            {
                context = dbContext;
            }

            public Task<string> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
