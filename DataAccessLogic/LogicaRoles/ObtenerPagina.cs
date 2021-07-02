using MediatR;
using Microsoft.EntityFrameworkCore;
using PersistenceData;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaRoles
{
    public class ObtenerPagina
    {
        public class Ejecuta : IRequest<ModificarPagina.Ejecuta>
        {
            [Required]
            public Guid PaginaId { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, ModificarPagina.Ejecuta>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext appDbContext)
            {
                context = appDbContext;
            }
            public async Task<ModificarPagina.Ejecuta> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var pagina = await context.Paginas.Where(p => p.PaginaId.Equals(request.PaginaId)).FirstOrDefaultAsync();
                if (pagina == null)
                    return null;
                return new ModificarPagina.Ejecuta
                {
                    PaginaId = pagina.PaginaId,
                    NombrePagina = pagina.NombrePagina,
                    Accion = pagina.Accion,
                    Controlador = pagina.Controlador
                };
            }
        }
    }
}
