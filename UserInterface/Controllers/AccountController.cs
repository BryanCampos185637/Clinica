using DataAccessLogic.Seguridad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccessLogic.LogicaUsuario;
using System.Threading.Tasks;
using Models;
using Microsoft.AspNetCore.Authorization;

namespace UserInterface.Controllers
{
    [AllowAnonymous]
    public class AccountController : MiControladorBaseController
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginIdentity.Ejecuta parametros)
        {
            if (ModelState.IsValid)
            {
                var rpt = await _mediator.Send(parametros);
                if (rpt)
                {
                    //Helpers.SessionHelper.crearCookieSession(HttpContext.Session, "login",
                    //   await _mediator.Send(new ObtenerUsuarioLogueado.Ejecuta
                    //   {
                    //       NombreUsuario = parametros.NombreUsuario,
                    //       Contra = parametros.Contra
                    //   }));
                    return Redirect("/Home/Index");
                }
                else
                {
                    TempData["success"] = "Usuario o contraseña incorrectos";
                    return View(parametros);
                }
            }
            else
                return View(parametros);
        }
        public IActionResult Registrar()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registrar(RegistrarIdentity.Ejecuta parametros)
        {
            if (ModelState.IsValid)
            {
               
                var rpt = await _mediator.Send(parametros);
                if (rpt=="Exito")
                {
                    return Redirect("Login");
                }
                else
                {
                    TempData["success"] = rpt;
                    return View(parametros);
                }
            }
            else
                return View(parametros);
        }
        
        public async Task<IActionResult> Cerrar()
        {
            var rpt = await _mediator.Send(new DataAccessLogic.Seguridad.CerrarSesion.Ejecuta());
            if(rpt)
                return Redirect("Login");
            else
                return Redirect("/Home/Index");
        }
    }
}
