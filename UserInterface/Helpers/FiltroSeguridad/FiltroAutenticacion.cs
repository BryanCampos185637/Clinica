using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models;

namespace UserInterface.Helpers.FiltroSeguridad
{
    public class FiltroAutenticacion : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            #region validacion existe la sesion
            var sesionActiva = SessionHelper.obtenerObjetoSesion<Usuario>(context.HttpContext.Session, "login");
            if (sesionActiva == null)
                context.Result = new RedirectResult("/Account/Login");//devolvemos a la vista login
            #endregion
        }
    }
}
