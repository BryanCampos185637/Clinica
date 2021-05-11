
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models;
using System;

namespace UserInterface.FiltroSeguridad
{
    public class FiltroDeSeguridadWeb : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var sesionActiva = Helpers.SessionHelper.obtenerObjetoSesion<Usuario>(context.HttpContext.Session, "login");
            if (sesionActiva == null)
                context.Result = new RedirectResult("/Login/Login");//devolvemos a la vista login
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var sesionActiva = Helpers.SessionHelper.obtenerObjetoSesion<Usuario>(context.HttpContext.Session, "login");
            if(sesionActiva==null)
                context.Result = new RedirectResult("/Login/Login");//devolvemos a la vista login
        }
    }
}
