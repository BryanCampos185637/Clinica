using MediatR;
using Microsoft.EntityFrameworkCore;
using PersistenceData;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaRoles
{
    public class EliminarRol
    {
        public class Ejecuta : IRequest<string>
        {
            public Guid Id { get; set; }
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
                using(var transaccionSQL = context.Database.BeginTransaction())
                {
                    try
                    {
                        #region validacion 
                        var rol = await context.TipoUsuarios.Where(p => p.TipoUsuarioId.Equals(request.Id)).FirstAsync();
                        if (rol.NombreTipoUsuario == "ADMINISTRADOR")
                            return "El rol administrador no se puede eliminar";
                        #endregion

                        #region eliminacion de la tabla PaginaTipoUsuario
                        var listaPaginaTipoUSuario = await context.PaginaTipoUsuarios.Where(p => p.TipoUsuarioId.Equals(request.Id)).ToListAsync();
                        foreach(var PaginaTipoUsuario in listaPaginaTipoUSuario)
                        {
                            context.PaginaTipoUsuarios.Remove(PaginaTipoUsuario);
                        }
                        #endregion

                        #region eliminacion del rol 
                        context.TipoUsuarios.Remove(rol);
                        #endregion

                        await context.SaveChangesAsync();
                        transaccionSQL.Commit();
                    }
                    catch (Exception e)
                    {
                        transaccionSQL.Rollback();
                        return "Error " + e.Message;
                    }
                }
                return "Exito";
            }
        }
    }
}
