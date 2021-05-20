using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using PersistenceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLogic.LogicaExpediente
{
    public class ObtenerListas
    {
        public class Ejecuta : IRequest<ListasParaExpedienteDTO> 
        {
            public bool esEditar { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, ListasParaExpedienteDTO>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext appDbContext)
            {
                context = appDbContext;
            }

            public async Task<ListasParaExpedienteDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    List<Paciente> listaPacientes=new List<Paciente>();
                    if (request.esEditar)
                    {
                        listaPacientes = await context.Pacientes.ToListAsync();
                    }
                    else
                    {
                        //nos retornara aquellos pacientes que no tengan asignado un expediente
                        listaPacientes = await context.Pacientes.Where(p=>p.PacienteTieneExpediente.Equals("NO") || p.PacienteTieneExpediente==null).ToListAsync();
                    }
                    var listaEnfermedades = await context.Enfermedades.ToListAsync();
                    return new ListasParaExpedienteDTO
                    {
                        ListaEnfermedad = listaEnfermedades,
                        ListaPaciente = listaPacientes
                    };
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
