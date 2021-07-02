using DataAccessLogic.LogicaUsuario;
using Microsoft.AspNetCore.Mvc;
using DataAccessLogic.LogicaTipoUsuario;
using System.Threading.Tasks;
using UserInterface.Helpers.FiltroSeguridad;
using Models;
using System;

namespace UserInterface.Controllers
{
    [ServiceFilter(typeof(FiltroAutenticacion))]
    public class UsuarioController : MiControladorBaseController
    {
        [ServiceFilter(typeof(FiltroAutorizacion))]
        public async Task<IActionResult> Index(string filtro = "", int pagina = 1, int cantidad = 5)
        {
            if (filtro == null) { filtro = ""; }
            ViewBag.filtro = filtro;
            return View(await _mediator.Send(new PaginarUsuario.Ejecuta
            {
                filtro = filtro,
                pagina = pagina,
                cantidadItems = cantidad
            }));
        }
        [ServiceFilter(typeof(FiltroAutorizacion))]
        public async Task<IActionResult> Guardar()
        {
            return View(new AgregarUsuario.Ejecuta
            {
                ListatipoUsuarios = await _mediator.Send(new ListarTipoUsuario.Ejecuta())
            });
        }
        [HttpPost]
        public async Task<IActionResult> Guardar(AgregarUsuario.Ejecuta parametros)
        {
            if (ModelState.IsValid)
            {
                parametros.Contra = Helpers.CifrarCadenas.cifrar(parametros.Contra);
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
                    parametros.ListatipoUsuarios = await _mediator.Send(new ListarTipoUsuario.Ejecuta());
                    return View(parametros);
                }
            }
            else
            {
                parametros.ListatipoUsuarios = await _mediator.Send(new ListarTipoUsuario.Ejecuta());
                return View(parametros);
            }
        }
        [ServiceFilter(typeof(FiltroAutorizacion))]
        public async Task<IActionResult> Editar(Guid id)
        {
            var obj = await _mediator.Send(new ObtenerUsuario.Ejecuta { UsuarioId = id });
            return View(new ModificarUsuario.Ejecuta
            {
                UsuarioId = obj.UsuarioId,
                NombreUsuario = obj.NombreUsuario,
                Contra = obj.Contra,
                TipoUsuarioId = obj.TipoUsuarioId,
                ListatipoUsuarios = await _mediator.Send(new ListarTipoUsuario.Ejecuta())
            });
        }
        [HttpPost]
        public async Task<IActionResult> Editar(ModificarUsuario.Ejecuta parametros)
        {
            if (ModelState.IsValid)
            {
                parametros.Contra = Helpers.CifrarCadenas.cifrar(parametros.Contra);
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
                    parametros.ListatipoUsuarios = await _mediator.Send(new ListarTipoUsuario.Ejecuta());
                    return View(parametros);
                }
            }
            else
            {
                parametros.ListatipoUsuarios = await _mediator.Send(new ListarTipoUsuario.Ejecuta());
                return View(parametros);
            }
        }
        [HttpPost]
        [ServiceFilter(typeof(FiltroAutorizacion))]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var idUsuario = Helpers.SessionHelper.obtenerObjetoSesion<Usuario>(HttpContext.Session, "login");
            if (idUsuario.UsuarioId == id)
            {
                TempData["success"] = "No puedes eliminar tu cuenta de usuario";
                TempData["icono"] = "warning";
                return Redirect("Index");
            }
            else
            {
                var rpt = await _mediator.Send(new EliminarUsuario.Ejecuta { UsuarioId = id });
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
}
