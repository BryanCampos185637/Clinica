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
    public class ModificarCita
    {
        public class Ejecuta : IRequest<string>
        {
            public Guid CitaId { get; set; }
            [Required]
            public Guid ExpedienteId { get; set; }
            [Required(ErrorMessage = "Debes seleccionar un servicio")]
            [Display(Name = "Servicio")]
            public Guid? ServicioId { get; set; }
            [Required(ErrorMessage = "Debes seleccionar una fecha")]
            [DataType(DataType.Date)]
            [Display(Name = "Fecha de cita")]
            public DateTime? FechaCita { get; set; }

            //solo de vista
            public List<Servicio> ListaServicio { get; set; }
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
                    var cita = await context.Citas.Where(p => p.CitaId == request.CitaId).FirstOrDefaultAsync();
                    cita.FechaCita = (DateTime)request.FechaCita;
                    cita.ServicioId = (Guid)request.ServicioId;
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
