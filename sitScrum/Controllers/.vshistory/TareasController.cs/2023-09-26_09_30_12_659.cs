using clbBusiness;
using Microsoft.AspNetCore.Mvc;
using System;

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
            return View();
        }
    }
}
