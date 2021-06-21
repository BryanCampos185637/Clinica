using MediatR;
using Microsoft.EntityFrameworkCore;
using PersistenceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaRoles
{
    public class ObtenerNombrePagina
    {
        public class Ejecuta : IRequest<string>
        {
            public string Accion { get; set; }
            public string Controlador { get; set; }
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
                var pagina = await context.Paginas.Where(p => p.Accion.Equals(request.Accion.ToUpper()) && p.Controlador.Equals(request.Controlador.ToUpper())).FirstOrDefaultAsync();
                if (pagina==null)
                    return "No se encontro el nombre";
                else
                    return pagina.NombrePagina;
            }
        }
    }
}
