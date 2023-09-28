using clbBusiness;
using clbBusinessInterface;
using clbModels;
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
            return View();
        }

        [HttpPost]
        public IActionResult ListarDetalle(Scrum_Tareas model)
        {
            try
            {
                var Result = _ITareasBO.Get(new clbModels.Bitacora.Scrum_Tareas());

                return PartialView("DetalleDatos", Result);
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

        [HttpPost]
        public IActionResult ModificarRegistro(Scrum_Tareas model)
        {
            try
            {
                ResponseDB Result;

                if (model.Accion.Equals("I"))
                {
                    Result = _ITareasBO.Set(model).FirstOrDefault();
                }
                else
                {
                    Result = _ITareasBO.Upd(model).FirstOrDefault();
                }

                if (Result != null)
                {
                    if (Result.existe_error.Equals("S"))
                    {
                        return BadRequest(_ITareasBO.ErrorMessage + " => " + Result.mensaje);
                    }
                }

                var Bolet = _ITareasBO.Get(model).FirstOrDefault();
                return PartialView("Row", Bolet);
            }
            catch (Exception ex)
            {
                return BadRequest(_ITareasBO.ErrorMessage + " => " + ex.Message);
            }
        }
    }
}
