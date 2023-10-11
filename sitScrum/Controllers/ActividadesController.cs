using clbBusinessInterface;
using clbModels.Bitacora;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace sitScrum.Controllers
{
    public class ActividadesController : Controller
    {
        private readonly IScrum_ActividadesBO _IActividadesBO;

        public ActividadesController(IScrum_ActividadesBO iActividadesBO)
        {
            _IActividadesBO = iActividadesBO;
        }
        public IActionResult Index()
        {

            // return View();

            try
            {
                var Result = _IActividadesBO.Get(new clbModels.Bitacora.Scrum_Actividades());

                return View(Result);
            }
            catch (Exception ex)
            {
                return BadRequest(_IActividadesBO.ErrorMessage + " => " + ex.Message);
            }

        }

        [HttpPost]
        public IActionResult ListarDetalle(Scrum_Actividades model)
        {
            try
            {
                var Result = _IActividadesBO.Get(new clbModels.Bitacora.Scrum_Actividades());

                return PartialView("DetalleDatos", Result);
            }
            catch (Exception ex)
            {
                return BadRequest(_IActividadesBO.ErrorMessage + " => " + ex.Message);
            }
        }

        public IActionResult RecuperarRegistro(Scrum_Actividades model)
        {
            try
            {
                var vAccion = model.Accion;
                var Result = new Scrum_Actividades() { Accion = vAccion };
                if (model.Cod_Actividad != null)
                {
                    Result = _IActividadesBO.Get(model).FirstOrDefault();
                    Result.Accion = vAccion;
                }

                return PartialView("ModalEditar", Result);
            }
            catch (Exception ex)
            {
                return BadRequest(_IActividadesBO.ErrorMessage + " => " + ex.Message);
            }
        }

    }

}

