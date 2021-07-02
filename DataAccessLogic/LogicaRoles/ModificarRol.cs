using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using PersistenceData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaRoles
{
    public class ModificarRol
    {
        public class Ejecuta : IRequest<string> 
        {
            [Required(ErrorMessage ="El id es requerido")]
            public Guid id { get; set; }
            [Required(ErrorMessage ="El nombre es requerido")]
            public string Nombre { get; set; }
            [Required(ErrorMessage = "La descripción es requerida")]
            [Display(Name ="Descripción")]
            public string Descripcion { get; set; }
            //propiedad extra
            [Required(ErrorMessage = "Debes asignarle permisos")]
            [Display(Name = "Asigna los permisos que desees")]
            public string arregloPaginaId { get; set; }
            public List<TipoUsuario>ListaTipoUsuario { get; set; }
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
                        #region validacion de rol
                        var EsRolAdministrador = await context.TipoUsuarios.Where(p => p.TipoUsuarioId == request.id).FirstOrDefaultAsync();
                        if (EsRolAdministrador.NombreTipoUsuario != request.Nombre)
                            return "No se puede modificar el Nombre a Administrador";


                        var existeRole = await context.TipoUsuarios.Where(p => p.NombreTipoUsuario.Equals(request.Nombre) && p.TipoUsuarioId != request.id).AnyAsync();
                        if (existeRole)
                            return request.Nombre + " ya existe en el sistema";
                        #endregion

                        #region modificacion
                        var rol = context.TipoUsuarios.Where(p => p.TipoUsuarioId.Equals(request.id)).First();
                        rol.NombreTipoUsuario = request.Nombre.ToUpper();
                        rol.DescripcionTipoUsuario = request.Descripcion.ToUpper();
                        var rpt = await context.SaveChangesAsync();
                        #endregion

                        #region asignar pagina
                        #region eliminar relaciones
                        var listaPaginasRelacionadas = await context.PaginaTipoUsuarios.Where(p => p.TipoUsuarioId.Equals(rol.TipoUsuarioId)).ToListAsync();
                        foreach (var item in listaPaginasRelacionadas)
                        {
                            context.PaginaTipoUsuarios.Remove(item);
                            await context.SaveChangesAsync();
                        }
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
