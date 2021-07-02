using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using PersistenceData;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaEnfermedad
{
    public class ModificarEnfermedad
    {
        /// <summary>
        /// propiedades que el usuario debe dar para poder ejecutar la transaccion
        /// </summary>
        public class Ejecuta : IRequest <string>
        {
            [Required]
            public Guid EnfermedadId { get; set; }
            [Display(Name = "NOMBRE")]
            [StringLength(200, ErrorMessage = "El nombre solo puede contener 200 caracteres")]
            [Required(ErrorMessage = "El nombre de la enfermedad es requerido")]
            public string NombreEnfermedad { get; set; }
            [Required(ErrorMessage = "La descripcíon de la enfermedad es requerida")]
            [StringLength(200, ErrorMessage = "La descripcíon solo puede contener 200 caracteres")]
            [Display(Name = "DESCRIPCÍON")]
            public string DescripcionEnfermedad { get; set; }
        }
        /// <summary>
        /// conecta con la base de datos y ejecuta la transaccion si todo esta bien debe devolver la palabra
        /// Exito
        /// </summary>
        public class Manejador : IRequestHandler<Ejecuta, string>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext _context)
            {
                context = _context;
            }
            public async Task<string> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    #region validacion
                    var nveces = await context.Enfermedades.Where(p => p.NombreEnfermedad.Equals(request.NombreEnfermedad)
                    && p.EnfermedadId != request.EnfermedadId).AnyAsync();
                    if (nveces)
                        return "La enfermedad ya esta registrada en la base de datos";
                    #endregion
                    var enfermedad = context.Enfermedades.Where(p => p.EnfermedadId == request.EnfermedadId).First();
                    enfermedad.NombreEnfermedad = request.NombreEnfermedad.ToUpper();
                    enfermedad.DescripcionEnfermedad = request.DescripcionEnfermedad.ToUpper();
                    await context.SaveChangesAsync();
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
