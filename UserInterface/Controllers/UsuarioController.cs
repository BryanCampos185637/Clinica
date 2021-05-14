using DataAccessLogic.LogicaUsuario;
using Microsoft.AspNetCore.Mvc;
using DataAccessLogic.LogicaTipoUsuario;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace UserInterface.Controllers
{
    //[ServiceFilter(typeof(FiltroDeSeguridadWeb))]
    [Authorize]
    public class UsuarioController : MiControladorBaseController
    {
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
        public async Task<IActionResult> Editar(int id)
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
        public async Task<IActionResult> Eliminar(int id)
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
