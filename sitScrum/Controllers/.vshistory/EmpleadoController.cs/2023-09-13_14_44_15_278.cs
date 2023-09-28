using Microsoft.AspNetCore.Mvc;

namespace sitScrum.Controllers
{
    public class EmpleadoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Saludar(int Id, string Nombre)
        {
            return View();
        }
    }
}
