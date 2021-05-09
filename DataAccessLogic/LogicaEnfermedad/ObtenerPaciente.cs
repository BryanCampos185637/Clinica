using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using PersistenceData;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaEnfermedad
{
    public class ObtenerPaciente
    {
        public class Ejecuta : IRequest<Enfermedad>
        {
            public Int64 EnfermedadId { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, Enfermedad>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext _context)
            {
                context = _context;
            }
            public async Task<Enfermedad> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                return await context.Enfermedades.Where(p => p.EnfermedadId == request.EnfermedadId).FirstOrDefaultAsync();
            }
        }
    }
}
