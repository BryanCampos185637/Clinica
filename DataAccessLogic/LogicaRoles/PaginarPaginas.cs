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

namespace DataAccessLogic.LogicaRoles
{
    public class PaginarPaginas
    {
        public class Ejecuta : IRequest<PaginaDTO> 
        {
            public int pagina { get; set; }
            public int cantidadItems { get; set; }
            public string filtro { get; set; }
        }
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
                    int totalAutoresActivos = context.Paginas.Where(p => p.NombrePagina.Contains(request.filtro)).Count();
                    int totalPaginas = (int)Math.Ceiling((double)totalAutoresActivos / request.cantidadItems);
                    if (request.pagina > totalPaginas) { request.pagina = totalPaginas; }
                    var list = await context.Paginas.Where(p => p.NombrePagina.Contains(request.filtro))
                                   .OrderByDescending(p => p.PaginaId)
                                   .Skip((request.pagina - 1) * request.cantidadItems)
                                   .Take(request.cantidadItems).ToListAsync();
                    return new PaginaDTO
                    {
                        lstPagina = list,
                        PaginaActual = request.pagina,
                        TotalRegistros = totalAutoresActivos,
                        RegistroPorPagina = request.cantidadItems,
                        TotalPaginas = totalPaginas,
                        Filtro = request.filtro
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
