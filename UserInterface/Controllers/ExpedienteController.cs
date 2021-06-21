using DataAccessLogic.LogicaExpediente;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UserInterface.Helpers.FiltroSeguridad;

namespace UserInterface.Controllers
{
    [ServiceFilter(typeof(FiltroAutenticacion))]
    public class ExpedienteController : MiControladorBaseController
    {
        [ServiceFilter(typeof(FiltroAutorizacion))]
        public async Task<IActionResult> Index(string filtro = "", int pagina = 1, int cantidad = 5)
        {
            if (filtro == null) { filtro = ""; }
            ViewBag.filtro = filtro;
            return View(await _mediator.Send(new PaginarExpediente.Ejecuta
            {
                filtro = filtro,
                pagina = pagina,
                cantidadItems = cantidad
            }));
        }
        [ServiceFilter(typeof(FiltroAutorizacion))]
        public async Task<IActionResult> Guardar()
        {
            var lst = await _mediator.Send(new ObtenerListas.Ejecuta { esEditar = false });
            return View(new AgregarExpediente.Ejecuta
            {
                ListaPaciente = lst.ListaPaciente,
                ListaEnfermedad = lst.ListaEnfermedad
            });
        }
        [HttpPost]
        public async Task<IActionResult> Guardar(AgregarExpediente.Ejecuta parametros)
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
                    var lst = await _mediator.Send(new ObtenerListas.Ejecuta { esEditar = false });
                    parametros.ListaEnfermedad = lst.ListaEnfermedad;
                    parametros.ListaPaciente = lst.ListaPaciente;
                    return View(parametros);
                }
            }
            else
            {
                var lst = await _mediator.Send(new ObtenerListas.Ejecuta { esEditar = false });
                parametros.ListaEnfermedad = lst.ListaEnfermedad;
                parametros.ListaPaciente = lst.ListaPaciente;
                return View(parametros);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var rpt = await _mediator.Send(new EliminarExpediente.Ejecuta { ExpedienteId = id });
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
        [ServiceFilter(typeof(FiltroAutorizacion))]
        public async Task<IActionResult> Editar(Guid id)
        {
            var obj = await _mediator.Send(new ObtenerExpediente.Ejecuta { ExpedienteId = id });
            var lst = await _mediator.Send(new ObtenerListas.Ejecuta { esEditar = true });
            return View(new ModificarExpediente.Ejecuta
            {
                ExpedienteId = obj.ExpedienteId,
                EnfermedadId = obj.EnfermedadId,
                PacienteId = obj.PacienteId,
                ListaEnfermedad = lst.ListaEnfermedad,
                ListaPaciente = lst.ListaPaciente
            });
        }
        [HttpPost]
        public async Task<IActionResult> Editar(ModificarExpediente.Ejecuta parametros)
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
                    var lst = await _mediator.Send(new ObtenerListas.Ejecuta { esEditar = true });
                    parametros.ListaEnfermedad = lst.ListaEnfermedad;
                    parametros.ListaPaciente = lst.ListaPaciente;
                    return View(parametros);
                }
            }
            else
            {
                var lst = await _mediator.Send(new ObtenerListas.Ejecuta());
                parametros.ListaEnfermedad = lst.ListaEnfermedad;
                parametros.ListaPaciente = lst.ListaPaciente;
                return View(parametros);
            }
        }
    }
}
