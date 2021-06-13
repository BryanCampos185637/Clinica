
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models.DTO;
using PersistenceData;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaRoles
{
    public class ListarPaginas
    {
        public class Ejecuta : IRequest<PaginaDTO> { }
        public class Manejador : IRequestHandler<Ejecuta, PaginaDTO>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext appDbContext)
            {
                context = appDbContext;
            }
            public async Task<PaginaDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    return new PaginaDTO
                    {
                        lstPagina = await context.Paginas.ToListAsync()
                    };
                }
                catch (System.Exception)
                {
                    return null;
                }
            }
        }
    }
}
