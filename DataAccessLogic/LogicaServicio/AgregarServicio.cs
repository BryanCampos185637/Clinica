using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using PersistenceData;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaServicio
{
    public class AgregarServicio
    {
        public class Ejecuta : IRequest<string>
        {
            [Display(Name ="NOMBRE")]
            [StringLength(200,ErrorMessage ="El nombre solo debe contener 200 caracteres")]
            [Required(ErrorMessage = "El nombre es requerido")]
            public string NombreServicio { get; set; }
            [Display(Name = "DESCRIPCÍON")]
            [StringLength(200, ErrorMessage = "La descripcíon solo debe contener 200 caracteres")]
            [Required(ErrorMessage = "La descripcíon es requerida")]
            public string DescripcionServicio { get; set; }
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
                    var existe = await context.Servicios.Where(p => p.NombreServicio.Equals(request.NombreServicio)).AnyAsync();
                    if (existe)
                        return "El servicio ya esta registrado en la base de datos";
                    context.Servicios.Add(new Servicio
                    {
                        DescripcionServicio = request.DescripcionServicio,
                        NombreServicio = request.NombreServicio,
                        FechaCreacion = DateTime.Now
                    });
                    var rpt = await context.SaveChangesAsync();
                    if (rpt > 0)
                        return "Exito";
                    else
                        return "No se pudo guardar el servicio";
                }
                catch (Exception e)
                {
                    return "Error " + e.Message;
                }
            }
        }
    }
}
