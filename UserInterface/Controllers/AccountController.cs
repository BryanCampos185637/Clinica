using DataAccessLogic.Seguridad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Login(ValidacionSesion.Ejecuta parametros)
        {
            if (ModelState.IsValid)
            {
                parametros.Contra = Helpers.CifrarCadenas.cifrar(parametros.Contra);
                var rpt = await _mediator.Send(parametros);
                if (rpt)
                {
                    Helpers.SessionHelper.crearCookieSession(HttpContext.Session, "login",
                       await _mediator.Send(new ObtenerUsuarioLogueado.Ejecuta
                       {
                           NombreUsuario = parametros.NombreUsuario,
                           Contra = parametros.Contra
                       }));
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
        public async Task<IActionResult> Registrar(RegistrarUsuario.Ejecuta parametros)
        {
            if (ModelState.IsValid)
            {
                parametros.Contra = Helpers.CifrarCadenas.cifrar(parametros.Contra);
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
        
        public  IActionResult Cerrar()
        {
            HttpContext.Session.Remove("login");
            return Redirect("Login");
        }
    }
}
