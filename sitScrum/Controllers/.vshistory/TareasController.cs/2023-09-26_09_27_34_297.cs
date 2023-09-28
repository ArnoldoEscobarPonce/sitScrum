using clbBusiness;
using Microsoft.AspNetCore.Mvc;

namespace sitScrum.Controllers
{
    public class TareasController : Controller
    {
        private readonly Scrum_TareasBO _ITareasBO;

        public TareasController(Scrum_TareasBO iTareasBO)
        {
            _ITareasBO = iTareasBO;
        }

        public IActionResult Index()
        {
            Scrum_TareasBO _TareasBO = new Scrum_TareasBO();
            return View();
        }
    }
}
