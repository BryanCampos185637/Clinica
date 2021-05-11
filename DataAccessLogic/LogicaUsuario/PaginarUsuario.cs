using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using PersistenceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaUsuario
{
    public class PaginarUsuario
    {
        public class Ejecuta : IRequest<UsuarioDTO>
        {
            public int pagina { get; set; }
            public int cantidadItems { get; set; }
            public string filtro { get; set; }

        }
        public class Manejador : IRequestHandler<Ejecuta, UsuarioDTO>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext dbContext)
            {
                context = dbContext;
            }

            public async Task<UsuarioDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    int totalActivos = context.Usuarios.Where(p => p.NombreUsuario.Contains(request.filtro)).Count();
                    int totalPaginas = (int)Math.Ceiling((double)totalActivos / request.cantidadItems);
                    if (request.pagina > totalPaginas) { request.pagina = totalPaginas; }
                    List<Usuario> list = await context.Usuarios.Where(p => p.NombreUsuario.Contains(request.filtro))
                                   .OrderByDescending(p => p.UsuarioId)
                                   .Include(p => p.TipoUsuario)
                                   .Skip((request.pagina - 1) * request.cantidadItems)
                                   .Take(request.cantidadItems).ToListAsync();
                    return new UsuarioDTO
                    {
                        ListaUsuario = list,
                        PaginaActual = request.pagina,
                        TotalRegistros = totalActivos,
                        RegistroPorPagina = request.cantidadItems,
                        TotalPaginas = totalPaginas,
                        Filtro = request.filtro
                    };
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
    }
}
