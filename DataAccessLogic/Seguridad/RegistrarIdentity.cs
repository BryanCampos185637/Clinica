using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersistenceData;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.Seguridad
{
    public class RegistrarIdentity
    {
        public class Ejecuta : IRequest<string>
        {
            [Display(Name ="NOMBRE DE USUARIO")]
            [Required(ErrorMessage = "El nombre de usuario es requerido")]
            public string Usuario { get; set; }
            [Required(ErrorMessage = "El email es requerido")]
            [EmailAddress]
            [Display(Name = "EMAIL")]
            public string Email { get; set; }
            [Required(ErrorMessage = "La contraseña es requerida")]
            [DataType(DataType.Password)]
            [Display(Name = "CONTRASEÑA")]
            public string password { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, string>
        {
            private readonly AppDbContext context;
            private readonly UserManager<IdentityUser> userManager;
            public Manejador(AppDbContext _context, UserManager<IdentityUser> _userManager)
            {
                context = _context;
                userManager = _userManager;
            }
            public async Task<string> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    var existe = await context.Users.Where(p => p.Email.Equals(request.Email)).AnyAsync();//deveulve bool
                    if (existe)
                        return "El email ingresado ya esta en uso";
                    var user = new IdentityUser
                    {
                        UserName = request.Email,
                        Email = request.Email
                    };
                    var rpt = await userManager.CreateAsync(user, request.password);
                    if (rpt.Succeeded)
                    {
                        return "Exito";
                    }
                    else
                    {
                        return "No se pudo crear el usuario";
                    }
                }
                catch (Exception e)
                {

                    throw new Exception("Error "+ e.Message);
                }
                
            }
        }
    }
}
