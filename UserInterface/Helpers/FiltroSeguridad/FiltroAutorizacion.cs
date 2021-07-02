using DataAccessLogic.LogicaRoles;
using DataAccessLogic.Seguridad;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models;

namespace UserInterface.Helpers.FiltroSeguridad
{
    public class FiltroAutorizacion : IActionFilter
    {
        private readonly IMediator mediator;
        public FiltroAutorizacion(IMediator mtr)
        {
            mediator = mtr;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var sesionActiva = SessionHelper.obtenerObjetoSesion<Usuario>(context.HttpContext.Session, "login");
            if (sesionActiva == null)
                context.Result = new RedirectResult("/Account/Login");//devolvemos a la vista login
            #region obtener los datos del controlador
            string DescripcionController = context.ActionDescriptor.DisplayName;//obtenemos la ruta
            var matriz = DescripcionController.Split('.');//hacemos una matriz siempre y cuando encuentre un punto
            string accion = matriz[3].Replace("(" + matriz[0] + ")", "").Trim();//obtenemos el nombre de la accion
            string controlador = matriz[2].Replace("Controller", "").Trim();//obtenemos el nombre del controlador
            #endregion

            #region autorizacion
            if (controlador.ToLower() != "home")
            {
                var tieneAcceso = mediator.Send(new ValidacionAcceso.Ejecuta { Usuario = sesionActiva, Accion = accion, Controlador = controlador }).Result;
                if (!tieneAcceso)
                {
                    var nombrePag = mediator.Send(new ObtenerNombrePagina.Ejecuta { Accion = accion, Controlador = controlador }).Result;
                    context.Result = new RedirectResult("/Account/Error401?pagina=" + nombrePag + "&paginaAnterior=" + controlador);//devolvemos a la vista login//devolvemos a la vista error
                }
                    
            }
            #endregion
        }
    }
}
