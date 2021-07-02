using DataAccessLogic.LogicaRoles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using UserInterface.Helpers.FiltroSeguridad;

namespace UserInterface.Controllers
{
    //[ServiceFilter(typeof(FiltroAutenticacion))]
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
            return Json(lista.lstPagina.OrderByDescending(p=>p.PaginaId));
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
        public async Task<IActionResult> Editar(Guid id)
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
        public async Task<JsonResult> DetallePaginasAsignadas(Guid id)
        {
            return Json(await _mediator.Send(new DetallePaginasAsignadas.Ejecuta { idRol = id }));
        }
        public IActionResult _DetallePaginasAsignadas()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var rpt = await _mediator.Send(new EliminarRol.Ejecuta { Id = id });
            if (rpt == "Exito")
            {
                TempData["success"] = "Se elimino correctamente";
                TempData["icono"] = "success";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["success"] = rpt;
                TempData["icono"] = "warning";
                return Redirect("Index");
            }
        }
    }
}
