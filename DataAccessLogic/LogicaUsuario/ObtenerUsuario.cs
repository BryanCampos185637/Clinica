using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using PersistenceData;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaUsuario
{
    public class ObtenerUsuario
    {
        public class Ejecuta : IRequest<Usuario>
        {
            public Guid UsuarioId { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, Usuario>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext dbContext)
            {
                context = dbContext;
            }
            public async Task<Usuario> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    return await context.Usuarios.Where(p => p.UsuarioId.Equals(request.UsuarioId)).FirstOrDefaultAsync();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
