using MediatR;
using Microsoft.EntityFrameworkCore;
using Models.DTO;
using PersistenceData;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaRoles
{
    public class PaginarRoles
    {
        public class Ejecuta : IRequest<RolDTO>
        {
            public int pagina { get; set; }
            public int cantidadItems { get; set; }
            public string filtro { get; set; }
        }
        /// <summary>
        /// este ejecuta la transaccion para crear el paginado
        /// </summary>
        public class Manejador : IRequestHandler<Ejecuta, RolDTO>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext _context)
            {
                context = _context;
            }

            public async Task<RolDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    int totalActivos = context.TipoUsuarios.Where(p => p.NombreTipoUsuario.Contains(request.filtro)).Count();
                    int totalPaginas = (int)Math.Ceiling((double)totalActivos / request.cantidadItems);
                    if (request.pagina > totalPaginas) { request.pagina = totalPaginas; }
                    var list = await context.TipoUsuarios.Where(p => p.NombreTipoUsuario.Contains(request.filtro))
                                   .OrderByDescending(p => p.TipoUsuarioId)
                                   .Skip((request.pagina - 1) * request.cantidadItems)
                                   .Take(request.cantidadItems).ToListAsync();
                    return new RolDTO
                    {
                        ListaTipoUsuario = list,
                        PaginaActual = request.pagina,
                        TotalRegistros = totalActivos,
                        RegistroPorPagina = request.cantidadItems,
                        TotalPaginas = totalPaginas,
                        Filtro = request.filtro
                    };
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
