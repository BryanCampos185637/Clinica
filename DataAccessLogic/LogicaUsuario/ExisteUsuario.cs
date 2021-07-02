using MediatR;
using Microsoft.EntityFrameworkCore;
using PersistenceData;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaUsuario
{
    public class ExisteUsuario
    {
        public class Ejecuta : IRequest<bool> { }
        public class Manejador : IRequestHandler<Ejecuta, bool>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext dbContext)
            {
                context = dbContext;
            }
            public async Task<bool> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    var total = await context.Usuarios.CountAsync();
                    if (total <= 0)
                        return false;
                }
                catch (System.Exception)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
