using Microsoft.AspNetCore.Mvc;
using Models;
using UserInterface.Helpers.FiltroSeguridad;

namespace UserInterface.Controllers
{
    [ServiceFilter(typeof(FiltroAutenticacion))]
    public class HomeController : MiControladorBaseController
    {
        public IActionResult Index()
        {
            ObtenerNombreUsuarioLogueado();
            return View();
        }
        private void ObtenerNombreUsuarioLogueado()
        {
            var usuarioLogueado = Helpers.SessionHelper.obtenerObjetoSesion<Usuario>(HttpContext.Session, "login");
            ViewBag.Nombre = usuarioLogueado.NombreCompleto;
        }
        
        public IActionResult _Menu()
        {
            return View();
        }
    }
}
