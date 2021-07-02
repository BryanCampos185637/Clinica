using DataAccessLogic.LogicaCitaMedica;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserInterface.Helpers.FiltroSeguridad;

namespace UserInterface.Controllers
{
    public class CitaMedicaController : MiControladorBaseController
    {
        //[ServiceFilter(typeof(FiltroAutorizacion))]
        public async Task<IActionResult> Index(string filtro = "", int pagina = 1, int cantidad = 5)
        {
            if (filtro == null) { filtro = ""; }
            ViewBag.filtro = filtro;
            return View(await _mediator.Send(new CitasDelDia.Ejecuta
            {
                filtro = filtro,
                pagina = pagina,
                cantidadItems = cantidad
            }));
        }
    }
}
