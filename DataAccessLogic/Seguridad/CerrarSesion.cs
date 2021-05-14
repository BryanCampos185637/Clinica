using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.Seguridad
{
    public class CerrarSesion
    {
        public class Ejecuta : IRequest<bool>{}
        public class Manejador : IRequestHandler<Ejecuta, bool>
        {
            private readonly SignInManager<IdentityUser> signInManager;
            public Manejador(SignInManager<IdentityUser> signIn)
            {
                signInManager = signIn;
            }
            public async Task<bool> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    await signInManager.SignOutAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
            }
        }
    }
}
