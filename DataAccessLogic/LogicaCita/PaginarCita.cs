using MediatR;
using Microsoft.EntityFrameworkCore;
using Models.DTO;
using PersistenceData;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaCita
{
    public class PaginarCita
    {
        /// <summary>
        /// devolvera un objeto de tipo EnfermedadDTO
        /// </summary>
        public class Ejecuta : IRequest<CitaDTO>
        {
            public int pagina { get; set; }
            public int cantidadItems { get; set; }
            public string filtro { get; set; }
        }
        /// <summary>
        /// este ejecuta la transaccion para crear el paginado
        /// </summary>
        public class Manejador : IRequestHandler<Ejecuta, CitaDTO>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext _context)
            {
                context = _context;
            }

            public async Task<CitaDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    int totalActivos = context.Citas.Include(p => p.Expediente.Paciente)
                                                    .Where(p => p.Expediente.Paciente.NoDuiPaciente.Contains(request.filtro)
                                                    && p.FechaCita > DateTime.Now).Count();
                    int totalPaginas = (int)Math.Ceiling((double)totalActivos / request.cantidadItems);
                    if (request.pagina > totalPaginas) { request.pagina = totalPaginas; }
                    var list = await context.Citas
                                   .Include(p => p.Expediente.Paciente)
                                   .Include(p => p.Expediente.Diagnostico)
                                   .Include(p => p.Servicio)
                                   .Where(p => p.Expediente.Paciente.NoDuiPaciente.Contains(request.filtro)
                                    && p.FechaCita > DateTime.Now)
                                   .OrderByDescending(p => p.FechaCita)
                                   .Skip((request.pagina - 1) * request.cantidadItems)
                                   .Take(request.cantidadItems).ToListAsync();
                    return new CitaDTO
                    {
                        ListaCitas = list,
                        PaginaActual = request.pagina,
                        TotalRegistros = totalActivos,
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
