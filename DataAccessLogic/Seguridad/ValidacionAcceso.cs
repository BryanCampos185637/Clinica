using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using PersistenceData;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.Seguridad
{
    public class ValidacionAcceso
    {
        public class Ejecuta: IRequest<bool>
        {
            public Usuario Usuario { get; set; }
            public string Accion { get; set; }
            public string Controlador { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, bool>
        {
            private readonly AppDbContext context;
            public Manejador( AppDbContext appDb)
            {
                context = appDb;
            }
            public async Task<bool> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    var pagina = await context.Paginas.Where(p => p.Accion.Equals(request.Accion) && p.Controlador.Equals(request.Controlador)).FirstOrDefaultAsync();
                    #region validacion de acceso
                    return await context.PaginaTipoUsuarios.Where(p => p.TipoUsuarioId.Equals(request.Usuario.TipoUsuarioId)
                                 && p.PaginaId.Equals(pagina.PaginaId)).AnyAsync();
                    #endregion
                }
                catch (System.Exception)
                {
                    return false;
                }
            }
        }
    }
}
