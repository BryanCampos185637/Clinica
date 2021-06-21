using DataAccessLogic.LogicaExpediente;
using DataAccessLogic.LogicaCita;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UserInterface.Helpers.FiltroSeguridad;

namespace UserInterface.Controllers
{
    [ServiceFilter(typeof(FiltroAutenticacion))]
    public class CitaController : MiControladorBaseController
    {
        [ServiceFilter(typeof(FiltroAutorizacion))]
        public async Task<IActionResult> Index(string filtro = "", int pagina = 1, int cantidad = 5)
        {
            if (filtro == null) { filtro = ""; }
            ViewBag.filtro = filtro;
            return View(await _mediator.Send(new PaginarCita.Ejecuta
            {
                filtro = filtro,
                pagina = pagina,
                cantidadItems = cantidad
            }));
        }
        [ServiceFilter(typeof(FiltroAutorizacion))]
        public async Task<IActionResult> CrearCita(string filtro = "", int pagina = 1, int cantidad = 5)
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
        public async Task<IActionResult> Guardar(Guid id)
        {
            var datosExpediente = await _mediator.Send(new ObtenerDatosExpediente.Ejecuta { ExpedienteId = id });
            return View(new AgregarCita.Ejecuta
            {
                ExpedienteId = id,
                ListaServicio = datosExpediente.ListaServicios,
                Enfermedad = datosExpediente.Enfermedad,
                Paciente = datosExpediente.Paciente
            });
        }
        [HttpPost]
        public async Task<IActionResult> Guardar(AgregarCita.Ejecuta parametros)
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
                    parametros = await llenarDatos(parametros);
                    return View(parametros);
                }
            }
            else
            {
                parametros = await llenarDatos(parametros);
                return View(parametros);
            }
        }
        private async Task<AgregarCita.Ejecuta>llenarDatos(AgregarCita.Ejecuta ejecuta)
        {
            var datosExpediente = await _mediator.Send(new ObtenerDatosExpediente.Ejecuta { ExpedienteId = ejecuta.ExpedienteId });
            ejecuta.ExpedienteId = ejecuta.ExpedienteId;
            ejecuta.ListaServicio = datosExpediente.ListaServicios;
            ejecuta.Enfermedad = datosExpediente.Enfermedad;
            ejecuta.Paciente = datosExpediente.Paciente;
            return ejecuta;

        }
        [HttpPost]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var rpt = await _mediator.Send(new EliminarCita.Ejecuta { CitaId = id });
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
            return View(await _mediator.Send(new ObtenerCitaPorId.Ejecuta { CitaId = id }));
        }
        [HttpPost]
        public async Task<IActionResult> Editar(ModificarCita.Ejecuta parametros)
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
                    var datos = await _mediator.Send(new ObtenerCitaPorId.Ejecuta { CitaId = parametros.CitaId });
                    parametros.Paciente = datos.Paciente;
                    parametros.ListaServicio = datos.ListaServicio;
                    parametros.Enfermedad = datos.Enfermedad;
                    TempData["success"] = rpt;
                    TempData["icono"] = "warning";
                    return View(parametros);
                }
            }
            else
            {
                var datos = await _mediator.Send(new ObtenerCitaPorId.Ejecuta { CitaId = parametros.CitaId });
                parametros.Paciente = datos.Paciente;
                parametros.ListaServicio = datos.ListaServicio;
                parametros.Enfermedad = datos.Enfermedad;
                return View(parametros);
            }
        }
    }
}
