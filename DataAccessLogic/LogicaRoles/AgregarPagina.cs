using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using PersistenceData;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaRoles
{
    public class AgregarPagina
    {
        public class Ejecuta : IRequest<string>
        {
            [Required(ErrorMessage ="El nombre de la pagina es requerido")]
            [Display(Name ="Nombre pagina")]
            [StringLength(50,ErrorMessage ="Solo admite 50 caracteres")]
            public string NombrePagina { get; set; }
            [Required(ErrorMessage = "La accion de la pagina es requerido")]
            [Display(Name = "Accion")]
            [StringLength(50, ErrorMessage = "Solo admite 50 caracteres")]
            public string Accion { get; set; }
            [Required(ErrorMessage = "El controlador de la pagina es requerido")]
            [Display(Name = "Controlador")]
            [StringLength(50, ErrorMessage = "Solo admite 50 caracteres")]
            public string Controlador { get; set; }
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
                    var existePagina = await context.Paginas.Where(p => p.Accion.Equals(request.Accion) && p.Controlador.Equals(request.Controlador)).AnyAsync();
                    if (existePagina)
                        return "La pagina ya existe en el sistema";
                    context.Paginas.Add(new Pagina
                    {
                        PaginaId = Guid.NewGuid(),
                        Accion = request.Accion.ToUpper(),
                        Controlador = request.Controlador.ToUpper(),
                        NombrePagina = request.NombrePagina.ToUpper()
                    });
                    await context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return "Error: No se pudo guardar la pagina, " + e.Message;
                }
                return "Exito";
            }
        }
    }
}
