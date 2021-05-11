using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using PersistenceData;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.Seguridad
{
    public class ObtenerUsuarioLogueado
    {
        public class Ejecuta : IRequest<Usuario>
        {
            public string NombreUsuario { get; set; }
            public string Contra { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, Usuario>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext appDbContext)
            {
                context = appDbContext;
            }

            public async Task<Usuario> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                return await context.Usuarios.Where(p => p.NombreUsuario.Equals(request.NombreUsuario)
                && p.Contra.Equals(request.Contra)).FirstOrDefaultAsync();
            }
        }
    }
}
