using MediatR;
using Microsoft.EntityFrameworkCore;
using PersistenceData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaRoles
{
    public class EliminarPagina
    {
        public class Ejecuta : IRequest<string>
        {
            [Required]
            public int PaginaId { get; set; }
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
                    #region validaciones 
                    var estaEnUso = await context.PaginaTipoUsuarios.Where(p => p.PaginaId.Equals(request.PaginaId)).AnyAsync();
                    if (estaEnUso)
                        return "No se puede eliminar, un rol la esta utilizando";
                    estaEnUso = await context.Botones.Where(p => p.PaginaId.Equals(request.PaginaId)).AnyAsync();
                    if (estaEnUso)
                        return "No se puede eliminar, un boton la esta utilizando";
                    #endregion

                    var obj = await context.Paginas.Where(p => p.PaginaId.Equals(request.PaginaId)).FirstAsync();
                    context.Paginas.Remove(obj);
                    var rpt = await context.SaveChangesAsync();
                    if (rpt <= 0)
                        return "No se pudo eliminar intente mas tarde";
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
