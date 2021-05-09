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
    public class AgregarEnfermedad
    {
        public class Ejecuta : IRequest<string>
        {
            [Display(Name = "NOMBRE")]
            [StringLength(200, ErrorMessage = "El nombre solo puede contener 200 caracteres")]
            [Required(ErrorMessage ="El nombre de la enfermedad es requerido")]
            public string NombreEnfermedad { get; set; }
            [Required(ErrorMessage = "La descripcíon de la enfermedad es requerida")]
            [StringLength(200,ErrorMessage = "La descripcíon solo puede contener 200 caracteres")]
            [Display(Name = "DESCRIPCÍON")]
            public string DescripcionEnfermedad { get; set; }
        }
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
                    var nveces = await context.Enfermedades.Where(p => p.NombreEnfermedad.Equals(request.NombreEnfermedad)).CountAsync();
                    if (nveces > 0)
                        return "Esta enfermedad ya esta registrada en el sistema";
                    context.Enfermedades.Add(new Enfermedad
                    {
                        NombreEnfermedad = request.NombreEnfermedad,
                        DescripcionEnfermedad = request.DescripcionEnfermedad,
                        FechaCreacion = DateTime.Now
                    });
                    var rpt = await context.SaveChangesAsync();
                    if (rpt > 0)
                        return "Exito";
                    else
                        return "No se pudo guardar la enfermedad";
                }
                catch(Exception e)
                {
                    return "Error " + e.Message;
                }
            }
        }
    }
}
