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

namespace DataAccessLogic.LogicaCita
{
    public class AgregarCita
    {
        public class Ejecuta : IRequest<string> 
        {
            [Required]
            public Guid ExpedienteId { get; set; }
            [Required(ErrorMessage ="Debes seleccionar un servicio")]
            [Display(Name ="Servicio")]
            public int? ServicioId { get; set; }
            [Required(ErrorMessage ="Debes seleccionar una fecha")]
            [DataType(DataType.Date)]
            [Display(Name = "Fecha de cita")]
            public DateTime? FechaCita { get; set; }

            //solo de vista
            public List<Servicio>ListaServicio { get; set; }
            public Paciente Paciente { get; set; }
            public Enfermedad Enfermedad { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, string>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext dbContext)
            {
                context = dbContext;
            }
            public async Task<string> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    var exiteCita = await context.Citas.Where(p => p.ExpedienteId.Equals(request.ExpedienteId) && p.FechaCita.Equals(request.FechaCita)).AnyAsync();
                    if (exiteCita)
                        return "El paciente ya tiene cita asignada para la fecha establecida";
                    context.Citas.Add(new Cita
                    {
                        CitaId = Guid.NewGuid(),
                        ExpedienteId = request.ExpedienteId,
                        FechaCita = (DateTime)request.FechaCita,
                        FechaCreacion = DateTime.UtcNow,
                        ServicioId = (int)request.ServicioId
                    });
                    var rpt = await context.SaveChangesAsync();
                    if (rpt <= 0)
                        return "No se pudo crear la cita";
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
