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

namespace DataAccessLogic.LogicaPaciente
{
    public class PaginarPaciente
    {
        public class Ejecuta : IRequest<PacienteDTO> 
        {
            public int pagina { get; set; }
            public int cantidadItems { get; set; }
            public string filtro { get; set; }
           
        }
        public class Manejador : IRequestHandler<Ejecuta, PacienteDTO>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext dbContext)
            {
                context = dbContext;
            }
            int totalAutoresActivos = 0, totalPaginas = 0;
            public async Task<PacienteDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    totalAutoresActivos = context.Pacientes.Where(p => p.NoDuiPaciente.Contains(request.filtro)).Count();
                    totalPaginas = (int)Math.Ceiling((double)totalAutoresActivos / request.cantidadItems);
                    var listPaciente = new List<Paciente>();
                    if (request.pagina > totalPaginas) { request.pagina = totalPaginas; }
                    listPaciente = await context.Pacientes.Where(p => p.NoDuiPaciente.Contains(request.filtro))
                                   .OrderByDescending(p => p.FechaCreacion)
                                   .Skip((request.pagina - 1) * request.cantidadItems)
                                   .Take(request.cantidadItems).ToListAsync();
                    var paciente = new PacienteDTO
                    {
                        ListaPacientes = listPaciente,
                        PaginaActual = request.pagina,
                        TotalRegistros = totalAutoresActivos,
                        RegistroPorPagina = request.cantidadItems,
                        TotalPaginas = totalPaginas,
                        Filtro = request.filtro
                    };
                    return paciente;
                }
                catch(Exception e)
                {
                    return null;
                }
            }
        }
    }
}
