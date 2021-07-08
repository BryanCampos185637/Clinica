using MediatR;
using Microsoft.EntityFrameworkCore;
using Models.DTO;
using PersistenceData;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaCitaMedica
{
    public class CitasDelDia
    {
        public class Ejecuta:IRequest<CitasDelDiaDTO>
        {
            public int pagina { get; set; }
            public int cantidadItems { get; set; }
            public string filtro { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, CitasDelDiaDTO>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext appDbContext)
            {
                context = appDbContext;
            }
            public async Task<CitasDelDiaDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    string fecha = DateTime.Now.ToString("dd/MM/yyyy");
                    var TotalCitas = await context.Citas.Where(p=>p.FechaCita == fecha).ToListAsync();
                    var TotalPaginas = (int)Math.Ceiling((double)TotalCitas.Count / request.cantidadItems);
                    if (request.pagina > TotalPaginas) { request.pagina = TotalPaginas; }
                    var ListaCita = await context.Citas.Where(p => p.FechaCita ==fecha)
                                                 .Include(p => p.Expediente)
                                                 .Include(p => p.Expediente.Paciente)
                                                 .Include(p => p.Servicio).ToListAsync();
                    return new CitasDelDiaDTO
                    {
                        Filtro = request.filtro,
                        RegistroPorPagina = request.cantidadItems,
                        ListaCita = ListaCita,
                        PaginaActual = request.pagina,
                        TotalRegistros = TotalCitas.Count,
                        TotalPaginas = TotalPaginas
                    };

                }
                catch (Exception e)
                {
                    string mensaje = e.Message;
                    return null;
                }
            }
        }
    }
}
