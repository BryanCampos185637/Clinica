using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using PersistenceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaServicio
{
    public class PaginarServicio
    {
        public class Ejecuta : IRequest<ServicioDTO>
        {
            public int pagina { get; set; }
            public int cantidadItems { get; set; }
            public string filtro { get; set; }

        }
        public class Manejador : IRequestHandler<Ejecuta, ServicioDTO>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext dbContext)
            {
                context = dbContext;
            }
            public async Task<ServicioDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    int totalActivos = context.Servicios.Where(p => p.NombreServicio.Contains(request.filtro)).Count();
                    int totalPaginas = (int)Math.Ceiling((double)totalActivos / request.cantidadItems);
                    var list = new List<Servicio>();
                    if (request.pagina > totalPaginas) { request.pagina = totalPaginas; }
                    list = await context.Servicios.Where(p => p.NombreServicio.Contains(request.filtro))
                                   .OrderByDescending(p => p.ServicioId)
                                   .Skip((request.pagina - 1) * request.cantidadItems)
                                   .Take(request.cantidadItems).ToListAsync();
                    return new ServicioDTO
                    {
                        listaServicios = list,
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
