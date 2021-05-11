using MediatR;
using Microsoft.EntityFrameworkCore;
using PersistenceData;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaUsuario
{
    public class EliminarUsuario
    {
        public class Ejecuta : IRequest<string>
        {
            public int UsuarioId { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, string>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext dbContext)
            {
                context = dbContext;
            }

            public async Task<string> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    var obj = await context.Usuarios.Where(p => p.UsuarioId.Equals(request.UsuarioId)).FirstOrDefaultAsync();
                    context.Usuarios.Remove(obj);
                    var rpt = await context.SaveChangesAsync();
                    if (rpt <= 0)
                        return "No se pudo eliminar el usuario";
                }
                catch (Exception e)
                {
                    return "Error " + e.Message;
                }
                return "Exito";
            }
        }
    }
}
