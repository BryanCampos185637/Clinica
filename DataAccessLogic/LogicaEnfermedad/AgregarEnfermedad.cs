﻿using MediatR;
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
        /// <summary>
        /// Contiene las propiedades que tendra que agregar el usuario
        /// </summary>
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
        /// <summary>
        /// conecta con la base de datos y ejecuta la transaccion
        /// si la transaccion es correcta debe devolver la palabra Exito
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
                    var nveces = await context.Enfermedades.Where(p => p.NombreEnfermedad.Equals(request.NombreEnfermedad)).CountAsync();
                    if (nveces > 0)
                        return "Esta enfermedad ya esta registrada en el sistema";
                    context.Enfermedades.Add(new Enfermedad
                    {
                        NombreEnfermedad = request.NombreEnfermedad.ToUpper(),
                        DescripcionEnfermedad = request.DescripcionEnfermedad.ToUpper(),
                        FechaCreacion = DateTime.Now,
                        EnfermedadId = Guid.NewGuid()
                    });
                    await context.SaveChangesAsync();
                }
                catch(Exception e)
                {
                    return "Error " + e.Message;
                }
                return "Exito";
            }
        }
    }
}
