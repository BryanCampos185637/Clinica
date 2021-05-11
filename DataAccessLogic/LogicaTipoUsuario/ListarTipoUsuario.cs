using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using PersistenceData;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaTipoUsuario
{
    public class ListarTipoUsuario
    {
        public class Ejecuta : IRequest<List<TipoUsuario>> { }
        public class Manejador : IRequestHandler<Ejecuta, List<TipoUsuario>>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext dbContext)
            {
                context = dbContext;
            }
            public async Task<List<TipoUsuario>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                return await context.TipoUsuarios
                    .Select(p => new TipoUsuario
                    {
                        TipoUsuarioId = p.TipoUsuarioId,
                        NombreTipoUsuario = p.NombreTipoUsuario,
                        DescripcionTipoUsuario = p.DescripcionTipoUsuario
                    }).OrderByDescending(p => p.TipoUsuarioId).ToListAsync();
            }
        }
    }
}
