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
using System.Transactions;

namespace DataAccessLogic.LogicaExpediente
{
    public class AgregarExpediente
    {
        public class Ejecuta : IRequest<string>
        {
            [Required(ErrorMessage ="El paciente es requerido")]
            [Display(Name ="Paciente")]
            public Guid PacienteId { get; set; }
            [Required(ErrorMessage = "La enfermedad es requerida")]
            [Display(Name = "Enfermedad")]
            public Guid? EnfermedadId { get; set; }
            //propiedades para viewModel
            public List<Enfermedad> ListaEnfermedad { get; set; }
            public List<Paciente> ListaPaciente { get; set; }
            public string NombreCompleto { get; set; }
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
                using (var transaccionSql = context.Database.BeginTransaction())
                {

                    try
                    {
                        var existeExpediente = await context.Expedientes.Where(p => p.PacienteId.Equals(request.PacienteId)).AnyAsync();
                        if (existeExpediente)
                            return "El paciente seleccionado ya cuenta con un expediente";

                        #region crear codigo generico
                        var paciente = await context.Pacientes.Where(p => p.PacienteId.Equals(request.PacienteId)).FirstAsync();
                        var arregloApellidos = paciente.ApellidoPaciente.Split(" ");
                        var codigoGenerado = Helper.GenerarCodigo.GenerarCodigoExpediente(paciente);
                        #endregion

                        #region llaves primarias de diagnostico y Expediente
                        var ExpedienteId = Guid.NewGuid();
                        var DiagnosticoId = Guid.NewGuid();
                        #endregion

                        #region creando un nuevo diagnostico
                        context.Diagnosticos.Add(new Diagnostico
                        {
                            DiagnosticoId = DiagnosticoId,
                            EnfermedadId = (Guid)request.EnfermedadId,
                            ExpedienteId = ExpedienteId,
                            FechaCreacion = DateTime.Now
                        });
                        #endregion

                        #region creando una nuevo expediente
                        context.Expedientes.Add(new Expediente
                        {
                            ExpedienteId = ExpedienteId,
                            DiagnosticoId = DiagnosticoId,
                            PacienteId = request.PacienteId,
                            FechaCreacion = DateTime.UtcNow,
                            CodidoExpediente = codigoGenerado
                        });
                        #endregion

                        #region modificamos la propiedad del paciente
                        paciente.PacienteTieneExpediente = "SI";
                        context.Pacientes.Update(paciente);
                        #endregion
                        await context.SaveChangesAsync();
                        transaccionSql.Commit();
                    }
                    catch (Exception e)
                    {
                        transaccionSql.Rollback();
                        return "Error" + e.Message;
                    }
                }
                return "Exito";
            }
        }
    }
}
