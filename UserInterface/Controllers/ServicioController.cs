using DataAccessLogic.LogicaServicio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserInterface.FiltroSeguridad;

namespace UserInterface.Controllers
{
    [ServiceFilter(typeof(FiltroDeSeguridadWeb))]
    public class ServicioController : MiControladorBaseController
    {
        public async Task<IActionResult> Index(string filtro = "", int pagina = 1, int cantidad = 5)
        {
            if (filtro == null) { filtro = ""; }
            ViewBag.filtro = filtro;
            return View(await _mediator.Send(new PaginarServicio.Ejecuta
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
        public async Task<IActionResult> Guardar(AgregarServicio.Ejecuta parametros)
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
            var servicio = await _mediator.Send(new ObtenerServicio.Ejecuta { ServicioId = id });
            return View(new ModificarServicio.Ejecuta
            {
                ServicioId = servicio.ServicioId,
                NombreServicio = servicio.NombreServicio,
                DescripcionServicio = servicio.DescripcionServicio
            });
        }
        [HttpPost]
        public async Task<IActionResult> Editar(ModificarServicio.Ejecuta parametros)
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
        public async Task<IActionResult> Eliminar(int id)
        {
            var rpt = await _mediator.Send(new EliminarServicio.Ejecuta { ServicioId = id });
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
