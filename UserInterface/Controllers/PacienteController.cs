using DataAccessLogic.LogicaExpediente;
using DataAccessLogic.LogicaPaciente;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UserInterface.Helpers.FiltroSeguridad;

namespace UserInterface.Controllers
{
    [ServiceFilter(typeof(FiltroAutenticacion))]
    public class PacienteController : MiControladorBaseController
    {
        public async Task<IActionResult> Index(string filtro = "", int pagina = 1, int cantidad = 5)
        {
            if (filtro == null) { filtro = ""; }
            ViewBag.filtro = filtro;
            return View(await _mediator.Send(new PaginarPaciente.Ejecuta
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
        public async Task<IActionResult> Guardar(AgregarPaciente.Ejecuta parametros)
        {
            if (ModelState.IsValid)
            {
                var rpt = await _mediator.Send(parametros);
                var matrizRespuesta = rpt.Split('$');
                if (matrizRespuesta[0] == "Exito")
                {
                    TempData["success"] = "Se guardo correctamente";
                    TempData["icono"] = "success";
                    return Redirect("/Paciente/CrearExpediente?id=" + matrizRespuesta[1]);
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
            var paciente = await _mediator.Send(new ObtenerPaciente.Ejecuta { PacienteId = id });
            return View(new ModificarPaciente.Ejecuta
            {
                PacienteId = paciente.PacienteId,
                NoDuiPaciente = paciente.NoDuiPaciente,
                NombrePaciente = paciente.NombrePaciente,
                ApellidoPaciente = paciente.ApellidoPaciente,
                EdadPaciente = paciente.EdadPaciente,
                Direccion=paciente.Direccion,
                FechaNacimiento=paciente.FechaNacimiento
            });
        }
        [HttpPost]
        public async Task<IActionResult> Editar(ModificarPaciente.Ejecuta parametros)
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
            var rpt = await _mediator.Send(new EliminarPaciente.Ejecuta { PacienteId = id });
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
        public async Task<IActionResult>CrearExpediente(Guid id)
        {
            try
            {
                var paciente = await _mediator.Send(new ObtenerPaciente.Ejecuta { PacienteId = id });
                var lst = await _mediator.Send(new ObtenerListas.Ejecuta { esEditar = false });
                return View(new AgregarExpediente.Ejecuta
                {
                    PacienteId= paciente.PacienteId,
                    ListaPaciente = lst.ListaPaciente,
                    ListaEnfermedad = lst.ListaEnfermedad,
                    NombreCompleto = paciente.NombrePaciente + " " + paciente.ApellidoPaciente
                });
            }
            catch(Exception e)
            {
                TempData["success"] = "Error " + e.Message;
                TempData["icono"] = "warning";
                return Redirect("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CrearExpediente(AgregarExpediente.Ejecuta parametros)
        {
            if (ModelState.IsValid)
            {
                var rpt = await _mediator.Send(parametros);
                if (rpt == "Exito")
                {
                    TempData["success"] = "Se guardo correctamente";
                    TempData["icono"] = "success";
                    return Redirect("/Expediente/Index");
                }
                else
                {
                    TempData["success"] = rpt;
                    TempData["icono"] = "warning";
                    #region obtener parametros
                    var lst = await _mediator.Send(new ObtenerListas.Ejecuta { esEditar = false });
                    var paciente = await _mediator.Send(new ObtenerPaciente.Ejecuta { PacienteId = parametros.PacienteId });
                    parametros.ListaEnfermedad = lst.ListaEnfermedad;
                    parametros.ListaPaciente = lst.ListaPaciente;
                    parametros.PacienteId = paciente.PacienteId;
                    parametros.NombreCompleto = paciente.NombrePaciente + " " + paciente.ApellidoPaciente;
                    #endregion
                    return View(parametros);
                }
            }
            else
            {
                #region obtener parametros
                var lst = await _mediator.Send(new ObtenerListas.Ejecuta { esEditar = false });
                var paciente = await _mediator.Send(new ObtenerPaciente.Ejecuta { PacienteId = parametros.PacienteId });
                parametros.ListaEnfermedad = lst.ListaEnfermedad;
                parametros.ListaPaciente = lst.ListaPaciente;
                parametros.PacienteId = paciente.PacienteId;
                parametros.NombreCompleto = paciente.NombrePaciente + " " + paciente.ApellidoPaciente;
                #endregion
                return View(parametros);
            }
        }
    }
}
