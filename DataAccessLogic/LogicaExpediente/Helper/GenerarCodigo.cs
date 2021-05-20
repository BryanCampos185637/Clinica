using Models;
using System;

namespace DataAccessLogic.LogicaExpediente.Helper
{
    public class GenerarCodigo
    {
        public static string GenerarCodigoExpediente(Paciente paciente)
        {
            var arregloApellidos = paciente.ApellidoPaciente.Split(" ");
            var codigoGenerado = "";
            if (arregloApellidos.Length > 1) 
            {
                for(var i =0; i < arregloApellidos.Length; i++)
                {
                    codigoGenerado += arregloApellidos[i].Substring(0, 1);
                }
            }
            else
            {
                codigoGenerado += arregloApellidos[0].Substring(0,2);
            }
            codigoGenerado += DateTime.Now.ToString("yy");
            codigoGenerado += "-";
            codigoGenerado += paciente.NoDuiPaciente;
            return codigoGenerado;
        }
    }
}
