using Microsoft.AspNetCore.Mvc;

namespace UserInterface.Controllers
{
    public class PaginadorController : Controller
    {
        public IActionResult _Paginador()
        {
            return View();
        }
    }
}
