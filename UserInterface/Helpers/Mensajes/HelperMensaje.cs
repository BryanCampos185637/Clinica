using Microsoft.AspNetCore.Mvc;

namespace UserInterface.Helpers.Mensajes
{
    public class HelperMensaje
    {
        public static void CrearMensaje(Controller controller, string Mensaje="Exito", string icono="success")
        {
            controller.TempData["success"] = Mensaje;
            controller.TempData["icono"] = icono;
        }
    }
}
