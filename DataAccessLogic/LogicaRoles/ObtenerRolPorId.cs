using MediatR;
using Microsoft.EntityFrameworkCore;
using PersistenceData;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaRoles
{
    public class ObtenerRolPorId
    {
        public class Ejecuta:IRequest<ModificarRol.Ejecuta> 
        {
            public int idRol { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, ModificarRol.Ejecuta>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext appDbContext)
            {
                context = appDbContext;
            }

            public async Task<ModificarRol.Ejecuta> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    var rol = await context.TipoUsuarios.Where(p => p.TipoUsuarioId.Equals(request.idRol)).FirstOrDefaultAsync();
                    var paginasAsignadas = await context.PaginaTipoUsuarios.Where(p => p.TipoUsuarioId.Equals(request.idRol)).ToListAsync();
                    string arreglo = "";
                    foreach(var item in paginasAsignadas)
                    {
                        arreglo += item.PaginaId.ToString() + "$";
                    }
                    return new ModificarRol.Ejecuta
                    {
                        id = rol.TipoUsuarioId,
                        Nombre = rol.NombreTipoUsuario,
                        Descripcion = rol.DescripcionTipoUsuario,
                        arregloPaginaId = arreglo
                    };
                }
                catch (System.Exception)
                {
                    return null;
                }
            }
        }
    }
}
