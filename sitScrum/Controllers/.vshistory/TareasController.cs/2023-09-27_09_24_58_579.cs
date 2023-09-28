using clbBusiness;
using clbBusinessInterface;
using clbModels.Bitacora;
using Microsoft.AspNetCore.Mvc;
using sitScrum.Models;
using System;
using System.Linq;
using System.Reflection;

namespace sitScrum.Controllers
{
    public class TareasController : Controller
    {
        private readonly IScrum_TareasBO _ITareasBO;

        public TareasController(IScrum_TareasBO iTareasBO)
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

        public IActionResult RecuperarRegistro(Scrum_Tareas model)
        {
            try
            {
                var vAccion = model.Accion;
                var Result = new Scrum_Tareas() { Accion = vAccion };
                if (model.Cod_Tarea != null)
                {
                    Result = _ITareasBO.Get(model).FirstOrDefault();
                    Result.Accion = vAccion;
                }

                return PartialView("ModalEditar", Result);
            }
            catch (Exception ex)
            {
                return BadRequest(_ITareasBO.ErrorMessage + " => " + ex.Message);
            }
        }
    }
}
