using clbBusiness;
using Microsoft.AspNetCore.Mvc;

namespace sitScrum.Controllers
{
    public class TareasController : Controller
    {
        private readonly Scrum_TareasBO _ITareasBO;
        public IActionResult Index()
        {
            return View();
        }
    }
}
