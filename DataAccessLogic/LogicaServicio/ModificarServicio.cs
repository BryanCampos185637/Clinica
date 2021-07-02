﻿using MediatR;
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
    public class ModificarServicio
    {
        public class Ejecuta : IRequest<string>
        {
            [Required]
            public Guid ServicioId { get; set; }
            [Display(Name = "NOMBRE")]
            [StringLength(200, ErrorMessage = "El nombre solo debe contener 200 caracteres")]
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
                    var existe = await context.Servicios.Where(p => p.NombreServicio.Equals(request.NombreServicio) 
                                && p.ServicioId!=request.ServicioId).AnyAsync();
                    if (existe)
                        return "El servicio ya esta registrado en la base de datos";
                    context.Servicios.Update(new Servicio
                    {
                        ServicioId=request.ServicioId,
                        DescripcionServicio = request.DescripcionServicio.ToUpper(),
                        NombreServicio = request.NombreServicio.ToUpper()
                    });
                    await context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return "Error: No se pudo modificar el servicio, " + e.Message;
                }
                return "Exito";
            }
        }
    }
}
