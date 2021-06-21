using DataAccessLogic.LogicaRoles;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserInterface.Helpers.FiltroSeguridad;

namespace UserInterface.Controllers
{
    [ServiceFilter(typeof(FiltroAutenticacion))]
    //[ServiceFilter(typeof(FiltroAutorizacion))]
    public class RolController : MiControladorBaseController
    {
        public async Task<IActionResult> Index(string filtro = "", int pagina = 1, int cantidad = 5)
        {
            if (filtro == null) { filtro = ""; }
            ViewBag.filtro = filtro;
            return View(await _mediator.Send(new PaginarRoles.Ejecuta
            {
                filtro = filtro,
                pagina = pagina,
                cantidadItems = cantidad
            }));
        }
        public IActionResult Guardar()
        {
            return View();
        }
        public async Task<JsonResult> ListarPaginas()
        {
            var lista = await _mediator.Send(new ListarPaginas.Ejecuta());
            return Json(lista.lstPagina);
        }
        [HttpPost]
        public async Task<IActionResult> Guardar(AgregarRol.Ejecuta parametros)
        {
            if (ModelState.IsValid)
            {
                var rpt = await _mediator.Send(parametros);
                if (rpt == "Exito")
                {
                    TempData["success"] = "Se guardo correctamente";
                    TempData["icono"] = "success";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["success"] = rpt;
                    TempData["icono"] = "warning";
                    return View(parametros);
                }
            }
            else
            {
                return View(parametros);
            }
        }
        public async Task<IActionResult> Editar(int id)
        {
            return View(await _mediator.Send(new ObtenerRolPorId.Ejecuta { idRol = id }));
        }
        [HttpPost]
        public async Task<IActionResult> Editar(ModificarRol.Ejecuta parametros)
        {
            if (ModelState.IsValid)
            {
                var rpt = await _mediator.Send(parametros);
                if (rpt == "Exito")
                {
                    TempData["success"] = "Se modifico correctamente";
                    TempData["icono"] = "success";
                    return RedirectToAction("Index");
                }
                else
                {
                    parametros.arregloPaginaId = await _mediator.Send(new ObtenerPaginasAsignadas.Ejecuta { idRol = parametros.id });
                    TempData["success"] = rpt;
                    TempData["icono"] = "warning";
                    return View(parametros);
                }
            }
            else
            {
                return View(parametros);
            }
        }
        [HttpGet]
        public async Task<JsonResult> DetallePaginasAsignadas(int id)
        {
            return Json(await _mediator.Send(new DetallePaginasAsignadas.Ejecuta { idRol = id }));
        }
        public IActionResult _DetallePaginasAsignadas()
        {
            return View();
        }
    }
}
