using Microsoft.AspNetCore.Mvc;
using sitScrum.Models;

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
            return View(new Persona() { Id = Id, Nombre = Nombre  });
        }
    }
}
