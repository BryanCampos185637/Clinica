using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using PersistenceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaRoles
{
    public class DetallePaginasAsignadas
    {
        public class Ejecuta : IRequest<List<Pagina>>
        {
            public int idRol { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, List<Pagina>>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext appDbContext)
            {
                context = appDbContext;
            }

            public async Task<List<Pagina>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    return await context.PaginaTipoUsuarios.Include(p => p.Pagina)
                        .Where(p => p.TipoUsuarioId.Equals(request.idRol))
                        .Select(p => new Pagina { NombrePagina = p.Pagina.NombrePagina }).ToListAsync();
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
    }
}
