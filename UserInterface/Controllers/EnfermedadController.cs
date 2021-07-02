using DataAccessLogic.LogicaEnfermedad;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UserInterface.Helpers.FiltroSeguridad;

namespace UserInterface.Controllers
{
    [ServiceFilter(typeof(FiltroAutenticacion))]
    public class EnfermedadController : MiControladorBaseController
    {
        [ServiceFilter(typeof(FiltroAutorizacion))]
        public async Task<IActionResult> Index(string filtro = "", int pagina = 1, int cantidad = 5)
        {
            if (filtro == null) { filtro = ""; }
            ViewBag.filtro = filtro;
            return View(await _mediator.Send(new PaginarEnfermedad.Ejecuta
            {
                filtro = filtro,
                pagina = pagina,
                cantidadItems = cantidad
            }));
        }
        [ServiceFilter(typeof(FiltroAutorizacion))]
        public IActionResult Guardar()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Guardar(AgregarEnfermedad.Ejecuta parametros)
        {
            if(ModelState.IsValid)
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
        [ServiceFilter(typeof(FiltroAutorizacion))]
        public async Task<IActionResult> Editar(Guid id)
        {
            var obj = await _mediator.Send(new ObtenerPaciente.Ejecuta { EnfermedadId = id });
            return View(new ModificarEnfermedad.Ejecuta
            {
                EnfermedadId = obj.EnfermedadId,
                NombreEnfermedad = obj.NombreEnfermedad,
                DescripcionEnfermedad = obj.DescripcionEnfermedad
            });
        }
        [HttpPost]
        public async Task<IActionResult> Editar(ModificarEnfermedad.Ejecuta parametros)
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
    }
}
