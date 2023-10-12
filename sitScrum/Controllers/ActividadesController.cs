using clbBusinessInterface;
using clbModels;
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

            return View();

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


        [HttpPost]
        public IActionResult ModificarRegistro(Scrum_Actividades model)
        {
            try
            {
                ResponseDB Result;

                if (model.Accion.Equals("I"))
                {
                    Result = _IActividadesBO.Set(model).FirstOrDefault();
                }
                else
                {
                    Result = _IActividadesBO.Upd(model).FirstOrDefault();
                }

                if (Result != null)
                {
                    if (Result.existe_error.Equals("S"))
                    {
                        return BadRequest(_IActividadesBO.ErrorMessage + " => " + Result.mensaje);
                    }
                }

                var Bolet = _IActividadesBO.Get(model).FirstOrDefault();
                return PartialView("Row", Bolet);
            }
            catch (Exception ex)
            {
                return BadRequest(_IActividadesBO.ErrorMessage + " => " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult EliminarRegistro(Scrum_Actividades model)
        {
            try
            {
                var vResponse = _IActividadesBO.Del(model).First();

                return Json(new { Mensaje = "Proceso Ejecutado", Error = "N", Cod_Transaccion = vResponse.cod_error });
            }
            catch (Exception ex)
            {
                return Json(new { Mensaje = $"Ocurrio un Problema {ex.Message}", Error = "S" });
            }
        }

    }

}

