using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models;

namespace UserInterface.Helpers.FiltroSeguridad
{
    public class FiltroAutenticacion : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var sesionActiva = SessionHelper.obtenerObjetoSesion<Usuario>(context.HttpContext.Session, "login");
            if (sesionActiva == null)
                context.Result = new RedirectResult("/Account/Login");//devolvemos a la vista login
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var sesionActiva = SessionHelper.obtenerObjetoSesion<Usuario>(context.HttpContext.Session, "login");
            if (sesionActiva == null)
                context.Result = new RedirectResult("/Account/Login");//devolvemos a la vista login
        }
    }
}
