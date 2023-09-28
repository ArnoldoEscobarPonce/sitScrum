using clbBusiness;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;

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
            //return View();

            try
            {
                var Result = _ITareasBO.Get(new clbModels.Bitacora.Scrum_Tareas());

                return View(Result);
            }
            catch (Exception ex)
            {
                return BadRequest(_ITareasBO.ErrorMessage + " => " + ex.Message);
            }
        }
    }
}
