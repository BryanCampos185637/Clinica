﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using PersistenceData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaUsuario
{
    public class ModificarUsuario
    {
        public class Ejecuta : IRequest<string>
        {
            [Required(ErrorMessage = "El id del usuario es requerido")]
            public Guid UsuarioId { get; set; }
            [Required(ErrorMessage ="El nombre de usuario es requerido")]
            [Display(Name ="NOMBRE USUARIO")]
            public string NombreUsuario { get; set; }
            //[Required(ErrorMessage = "La contraseña es requerida")]
            [Display(Name = "CONTRASEÑA")]
            [DataType(DataType.Password)]
            public string Contra { get; set; }
            [Display(Name = "ROL")]
            [Required(ErrorMessage = "El rol es requerido")]
            public Guid TipoUsuarioId { get; set; }
            public List<TipoUsuario> ListatipoUsuarios { get; set; }
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
                    var exite = await context.Usuarios.Where(p => p.NombreUsuario.Equals(request.NombreUsuario) 
                                        && p.UsuarioId!=request.UsuarioId).AnyAsync();
                    if (exite)
                        return "El nombre de usuario ya esta en uso";
                    var user = await context.Usuarios.Where(p => p.UsuarioId == request.UsuarioId).FirstOrDefaultAsync();
                    user.NombreUsuario = request.NombreUsuario.ToUpper();
                    if (request.Contra != null && request.Contra != "") 
                        user.Contra = request.Contra;
                    user.TipoUsuarioId = request.TipoUsuarioId;
                    await context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return "Error: No se pudo guardar el usuario, " + e.Message;
                }
                return "Exito";
            }
        }
    }
}
