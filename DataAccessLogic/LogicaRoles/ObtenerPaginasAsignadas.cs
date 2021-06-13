using MediatR;
using Microsoft.EntityFrameworkCore;
using PersistenceData;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaRoles
{
    public class ObtenerPaginasAsignadas
    {
        public class Ejecuta : IRequest<string>
        {
            public int idRol { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, string>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext appDbContext)
            {
                context = appDbContext;
            }

            public async Task<string> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    var paginasAsignadas = await context.PaginaTipoUsuarios.Where(p => p.TipoUsuarioId.Equals(request.idRol)).ToListAsync();
                    string arreglo = "";
                    foreach (var item in paginasAsignadas)
                    {
                        arreglo += item.PaginaId.ToString() + "$";
                    }
                    return arreglo;
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }
    }
}
