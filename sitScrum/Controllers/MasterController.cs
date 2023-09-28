using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sitScrum.Models.Authentication;
using sitScrum.util;
using IMSAUtil.Sessions;
using System;
using System.Web;


namespace sitScrum.Controllers
{
    public class MasterController : Controller
    {

        public void Prepara_Mensaje(string[] pMensaje, bool tempData = false)
        {
            if (tempData)
            {
                TempData["CodMensajeBD"] = pMensaje[0];
                TempData["MensajeBD"] = pMensaje[1];
            }
            else
            {
                ViewBag.CodMensajeBD = pMensaje[0];
                ViewBag.MensajeBD = pMensaje[1];
            }
        }

        public void VerificaMensajesTempData()
        {

            if (TempData["CodMensajeBD"] != null)
            {

                ViewBag.CodMensajeBD = TempData["CodMensajeBD"];
                ViewBag.MensajeBD = TempData["MensajeBD"];

            }

        }

        public DatosUsuario GetDatosUsuario()
        {
            var vDatos = this.HttpContext.Session.GetObject<DatosUsuario>("usrData");
            return vDatos;
        }

        public string getUsr()
        {

            var Usuario = this.GetDatosUsuario();
            if (Usuario != null)
            {
                return Usuario.Usuario;
            }

            return "";
        }
    }
}