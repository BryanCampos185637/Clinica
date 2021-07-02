using DataAccessLogic.LogicaRoles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UserInterface.Helpers.FiltroSeguridad;

namespace UserInterface.Controllers
{
    [ServiceFilter(typeof(FiltroAutenticacion))]
    public class PaginaController : MiControladorBaseController
    {
        [ServiceFilter(typeof(FiltroAutorizacion))]
        public async Task<IActionResult> Index(string filtro = "", int pagina = 1, int cantidad = 5)
        {
            if (filtro == null) { filtro = ""; }
            ViewBag.filtro = filtro;
            return View(await _mediator.Send(new PaginarPaginas.Ejecuta
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
        [HttpPost]
        public async Task<IActionResult> Guardar(AgregarPagina.Ejecuta parametros)
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
            return View(await _mediator.Send(new ObtenerPagina.Ejecuta { PaginaId = id }));
        }
        [HttpPost]
        public async Task<IActionResult> Editar(ModificarPagina.Ejecuta parametros)
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
        [HttpPost]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var rpt = await _mediator.Send(new EliminarPagina.Ejecuta { PaginaId = id });
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
