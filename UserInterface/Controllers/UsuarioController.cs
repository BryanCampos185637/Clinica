using DataAccessLogic.LogicaUsuario;
using Microsoft.AspNetCore.Mvc;
using DataAccessLogic.LogicaTipoUsuario;
using System.Threading.Tasks;

namespace UserInterface.Controllers
{
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
        public IActionResult Guardar()
        {
            return View();
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
                    return View(parametros);
                }
            }
            else
            {
                return View(parametros);
            }
        }
        [HttpGet]
        public async Task<JsonResult> ListarTipoUsuario()
        {
            var lst = await _mediator.Send(new ListarTipoUsuario.Ejecuta());
            return Json(lst);
        }
        public async Task<IActionResult> Editar(int id)
        {
            var obj = await _mediator.Send(new ObtenerUsuario.Ejecuta { UsuarioId = id });
            return View(new ModificarUsuario.Ejecuta
            {
                UsuarioId = obj.UsuarioId,
                NombreUsuario = obj.NombreUsuario,
                Contra = obj.Contra,
                TipoUsuarioId = obj.TipoUsuarioId
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
