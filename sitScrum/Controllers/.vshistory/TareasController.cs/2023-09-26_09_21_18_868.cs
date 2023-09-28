using Microsoft.AspNetCore.Mvc;

namespace sitScrum.Controllers
{
    public class TareasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
