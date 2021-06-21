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
    public class AgregarUsuario
    {
        public class Ejecuta : IRequest<string>
        {
            [Required(ErrorMessage = "El nombre es requerido")]
            [Display(Name = "Nombre completo")]
            public string NombreCompleto { get; set; }
            [Required(ErrorMessage = "La edad es requerida")]
            [Display(Name = "Edad")]
            [Range(1, 150, ErrorMessage = "La edad debe estar entre 1-150")]
            public int? Edad { get; set; }
            [Display(Name = "Direccion")]
            [Required(ErrorMessage = "La dirección es requerida")]
            public string Direccion { get; set; }
            [Required(ErrorMessage = "El nombre de usuario es requerido")]
            [Display(Name = "Nombre usuario")]
            public string NombreUsuario { get; set; }
            [Required(ErrorMessage = "La contraseña es requerida")]
            [Display(Name = "Contraseña")]
            [DataType(DataType.Password)]
            public string Contra { get; set; }
            [Required(ErrorMessage = "El tipo de usuario es requerido")]
            [Display(Name = "Rol")]
            public int? TipoUsuarioId { get; set; }
            //lista para mostrar los tipos de usuario
            public List<TipoUsuario>ListatipoUsuarios { get; set; }
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
                    var exite = await context.Usuarios.Where(p => p.NombreUsuario.Equals(request.NombreUsuario)).AnyAsync();
                    if (exite)
                        return "El nombre de usuario ya esta en uso";
                    context.Usuarios.Add(new Usuario
                    {
                        NombreCompleto = request.NombreCompleto.ToUpper(),
                        NombreUsuario = request.NombreUsuario.ToUpper(),
                        Edad = Convert.ToInt32(request.Edad),
                        Contra = request.Contra,
                        Direccion = request.Direccion.ToUpper(),
                        TipoUsuarioId = (int)request.TipoUsuarioId
                    });
                    var rpt = await context.SaveChangesAsync();
                    if (rpt <= 0)
                        return "No se pudo modificar el usuario";
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
