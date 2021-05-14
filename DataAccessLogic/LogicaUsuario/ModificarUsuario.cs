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

namespace DataAccessLogic.LogicaUsuario
{
    public class ModificarUsuario
    {
        public class Ejecuta : IRequest<string>
        {
            [Required(ErrorMessage = "El id del usuario es requerido")]
            public int UsuarioId { get; set; }
            [Required(ErrorMessage ="El nombre de usuario es requerido")]
            [Display(Name ="NOMBRE USUARIO")]
            public string NombreUsuario { get; set; }
            [Required(ErrorMessage = "La contraseña es requerida")]
            [Display(Name = "CONTRASEÑA")]
            [DataType(DataType.Password)]
            public string Contra { get; set; }
            [Display(Name = "ROL")]
            [Required(ErrorMessage = "El rol es requerido")]
            public int? TipoUsuarioId { get; set; }
            public List<TipoUsuario> ListatipoUsuarios { get; set; }
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
                    var exite = await context.Usuarios.Where(p => p.NombreUsuario.Equals(request.NombreUsuario) 
                                        && p.UsuarioId!=request.UsuarioId).AnyAsync();
                    if (exite)
                        return "El nombre de usuario ya esta en uso";
                    context.Usuarios.Update(new Usuario
                    {
                        UsuarioId=request.UsuarioId,
                        NombreUsuario = request.NombreUsuario.ToUpper(),
                        Contra = request.Contra,
                        TipoUsuarioId = (int)request.TipoUsuarioId
                    });
                    var rpt = await context.SaveChangesAsync();
                    if (rpt <= 0)
                        return "No se pudo guardar el usuario";
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
