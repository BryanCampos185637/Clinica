using DataAccessLogic.Seguridad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccessLogic.LogicaUsuario;
using System.Threading.Tasks;

namespace UserInterface.Controllers
{
    public class LoginController : MiControladorBaseController
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
        public async Task<IActionResult> Registrar(AgregarUsuario.Ejecuta parametros)
        {
            if (ModelState.IsValid)
            {
                parametros.TipoUsuarioId = 1;
                parametros.Contra = Helpers.CifrarCadenas.cifrar(parametros.Contra);
                var rpt = await _mediator.Send(parametros);
                if (rpt=="Exito")
                {
                    return Redirect("/Login/Login");
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
    }
}
