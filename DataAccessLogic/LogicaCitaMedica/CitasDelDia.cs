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
                    var TotalCitas = context.Citas.Where(p => p.FechaCita.ToString("yyyy/MM/dd").Contains(DateTime.Now.ToString("yyyy/MM/dd"))).Count();
                    var TotalPaginas = (int)Math.Ceiling((double)TotalCitas / request.cantidadItems);
                    if (request.pagina > TotalPaginas) { request.pagina = TotalPaginas; }
                    var ListaCita = await context.Citas.Where(p=> DbFunctions.Equals(p.FechaCita, DateTime.Now))
                                                 .Include(p => p.Expediente).Include(p => p.Expediente.Paciente).ToListAsync();
                    return new CitasDelDiaDTO
                    {
                        Filtro = request.filtro,
                        RegistroPorPagina = request.cantidadItems,
                        ListaCita = ListaCita,
                        PaginaActual = request.pagina,
                        TotalRegistros = TotalCitas,
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
