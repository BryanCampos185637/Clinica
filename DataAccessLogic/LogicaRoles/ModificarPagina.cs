using MediatR;
using Microsoft.EntityFrameworkCore;
using PersistenceData;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaRoles
{
    public class ModificarPagina
    {
        public class Ejecuta : IRequest<string>
        {
            [Required]
            public int PaginaId { get; set; }
            [Required(ErrorMessage = "El nombre de la pagina es requerido")]
            [Display(Name = "Nombre pagina")]
            [StringLength(50, ErrorMessage = "Solo admite 50 caracteres")]
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
                    #region validar que exista la pagina en el sistema
                    var obj = await context.Paginas.Where(p => p.PaginaId.Equals(request.PaginaId)).FirstOrDefaultAsync();
                    if (obj == null)
                        return "No hay ninguna pagina que coincida con el id";
                    #endregion

                    #region validar que no se repita la pagina en el sistema
                    var existePagina = await context.Paginas.Where(p => p.Accion.Equals(request.Accion) && p.Controlador.Equals(request.Controlador)
                    && p.PaginaId != request.PaginaId).AnyAsync();
                    if (existePagina)
                        return "La pagina ya existe en el sistema";
                    #endregion

                    #region guardar cambios
                    obj.Accion = request.Accion.ToUpper();
                    obj.Controlador = request.Controlador.ToUpper();
                    obj.NombrePagina = request.NombrePagina.ToUpper();
                    var rpt = await context.SaveChangesAsync();
                    #endregion

                    if (rpt <= 0)
                        return "No se pudo modificar la pagina";
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
