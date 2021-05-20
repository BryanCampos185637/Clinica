using MediatR;
using Microsoft.EntityFrameworkCore;
using Models.DTO;
using PersistenceData;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaExpediente
{
    public class PaginarExpediente
    {
        public class Ejecuta : IRequest<ExpedienteDTO> 
        {
            public int pagina { get; set; }
            public int cantidadItems { get; set; }
            public string filtro { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, ExpedienteDTO>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext dbContext)
            {
                context = dbContext;
            }
            public async Task<ExpedienteDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                ExpedienteDTO expedienteDTO;
                try
                {
                    var totalAutoresActivos = context.Expedientes.Include(p => p.Paciente).Where(p => p.CodidoExpediente.Contains(request.filtro)).Count();
                    var totalPaginas = (int)Math.Ceiling((double)totalAutoresActivos / request.cantidadItems);
                    if (request.pagina > totalPaginas) { request.pagina = totalPaginas; }
                    var lst = await context.Expedientes.Include(p=>p.Paciente).Include(p=>p.Enfermedad)
                                   .Where(p => p.CodidoExpediente.Contains(request.filtro))
                                   .OrderByDescending(p => p.PacienteId)
                                   .Skip((request.pagina - 1) * request.cantidadItems)
                                   .Take(request.cantidadItems).ToListAsync();
                    expedienteDTO = new ExpedienteDTO
                    {
                        ListaExpediente = lst,
                        PaginaActual = request.pagina,
                        TotalRegistros = totalAutoresActivos,
                        RegistroPorPagina = request.cantidadItems,
                        TotalPaginas = totalPaginas,
                        Filtro = request.filtro
                    };
                }
                catch (Exception)
                {
                    expedienteDTO = null;
                }
                return expedienteDTO;
            }
        }
    }
}
