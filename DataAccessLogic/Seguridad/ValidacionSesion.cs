using MediatR;
using Microsoft.EntityFrameworkCore;
using PersistenceData;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.Seguridad
{
    public class ValidacionSesion
    {
        public class Ejecuta : IRequest<bool>
        {
            [Display(Name ="NOMBRE USUARIO")]
            [Required(ErrorMessage ="El nombre usuario es requerido")]
            public string NombreUsuario { get; set; }
            [Display(Name = "CONTRASEÑA")]
            [Required(ErrorMessage = "La contraseña es requerida")]
            [DataType(DataType.Password)]
            public string Contra { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, bool>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext appDbContext)
            {
                context = appDbContext;
            }
            public async Task<bool> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    var exiteUsuario = await context.Usuarios.Where(p => p.NombreUsuario.Equals(request.NombreUsuario)).FirstOrDefaultAsync();
                    if (exiteUsuario == null)
                        return false;
                    if (exiteUsuario.Contra != request.Contra)
                        return false;
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
    }
}
