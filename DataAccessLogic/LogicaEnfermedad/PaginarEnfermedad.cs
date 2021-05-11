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

namespace DataAccessLogic.LogicaEnfermedad
{
    public class PaginarEnfermedad
    {
        /// <summary>
        /// devolvera un objeto de tipo EnfermedadDTO
        /// </summary>
        public class Ejecuta:IRequest<EnfermedadDTO>
        {
            public int pagina { get; set; }
            public int cantidadItems { get; set; }
            public string filtro { get; set; }
        }
        /// <summary>
        /// este ejecuta la transaccion para crear el paginado
        /// </summary>
        public class Manejador : IRequestHandler<Ejecuta, EnfermedadDTO>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext _context)
            {
                context = _context;
            }
            public async Task<EnfermedadDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    int totalAutoresActivos = context.Enfermedades.Where(p => p.NombreEnfermedad.Contains(request.filtro)).Count();
                    int totalPaginas = (int)Math.Ceiling((double)totalAutoresActivos / request.cantidadItems);
                    var list = new List<Enfermedad>();
                    if (request.pagina > totalPaginas) { request.pagina = totalPaginas; }
                    list = await context.Enfermedades.Where(p => p.NombreEnfermedad.Contains(request.filtro))
                                   .OrderByDescending(p => p.EnfermedadId)
                                   .Skip((request.pagina - 1) * request.cantidadItems)
                                   .Take(request.cantidadItems).ToListAsync();
                    return new EnfermedadDTO
                    {
                        listaEnfermedad = list,
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
