using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using PersistenceData;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccessLogic.LogicaRoles
{
    public class AgregarRol
    {
        public class Ejecuta : IRequest<string> 
        {
            [Required(ErrorMessage ="El nombre es requerido")]
            public string Nombre { get; set; }
            [Required(ErrorMessage = "La descripción es requerida")]
            [Display(Name ="Descripción")]
            public string Descripcion { get; set; }
            [Required(ErrorMessage = "Debes asignarle permisos")]
            [Display(Name = "Asigna los permisos que desees")]
            public string arregloPaginaId { get; set; }
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
                using (var transaccionSql = context.Database.BeginTransaction())
                {
                    try
                    {
                        #region guardar rol
                        var existeRole = await context.TipoUsuarios.Where(p => p.NombreTipoUsuario.Equals(request.Nombre)).AnyAsync();
                        if (existeRole)
                            return request.Nombre + " ya existe en el sistema";
                        var rol = new TipoUsuario
                        {
                            NombreTipoUsuario = request.Nombre.ToUpper(),
                            DescripcionTipoUsuario = request.Descripcion.ToUpper(),
                            FechaCreacion = DateTime.Now
                        };
                        context.TipoUsuarios.Add(rol);
                        var rpt = await context.SaveChangesAsync();
                        #endregion

                        #region relacionar paginas
                        var listaPaginasSeleccionadas = request.arregloPaginaId.Substring(0, request.arregloPaginaId.Length - 1).Split('$');
                        foreach (var item in listaPaginasSeleccionadas)
                        {
                            if (item != null || item != "")
                            {
                                var paginaTipoUsuario = new PaginaTipoUsuario
                                {
                                    PaginaId = new Guid(item),
                                    TipoUsuarioId = rol.TipoUsuarioId
                                };
                                context.PaginaTipoUsuarios.Add(paginaTipoUsuario);
                                await context.SaveChangesAsync();
                            }
                        }
                        #endregion

                        transaccionSql.Commit();
                    }
                    catch (Exception e)
                    {
                        transaccionSql.Rollback();
                        return "Error " + e.Message;
                    }
                }
                return "Exito";
            }
        }
    }
}
