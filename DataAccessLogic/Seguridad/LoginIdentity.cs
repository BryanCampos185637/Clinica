using MediatR;
using Microsoft.AspNetCore.Identity;
using PersistenceData;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.Seguridad
{
    public class LoginIdentity
    {
        public class Ejecuta : IRequest<bool>
        {
            [Required(ErrorMessage ="El email es requerido")]
            [EmailAddress]
            [Display(Name ="CORREO")]
            public string Email { get; set; }
            [Required(ErrorMessage ="La contraseña es requerida")]
            [DataType(DataType.Password)]
            [Display(Name = "CONTRASEÑA")]
            public string password { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, bool>
        {
            private readonly UserManager<IdentityUser> userManager;
            private readonly SignInManager<IdentityUser> signInManager;

            public Manejador(UserManager<IdentityUser> user, SignInManager<IdentityUser> signIn)
            {
                userManager = user;
                signInManager = signIn;
            }
            public async Task<bool> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var usuario = await userManager.FindByEmailAsync(request.Email);
                if (usuario == null)
                {
                    return false;
                }
                //validamos la contraseña
                var rpt = await signInManager.CheckPasswordSignInAsync(usuario, request.password, false);
                if (rpt.Succeeded)
                {
                    var result = await signInManager.PasswordSignInAsync(request.Email, request.password, false, false);
                    if (result.Succeeded)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
