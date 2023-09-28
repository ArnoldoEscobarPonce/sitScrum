using IMSA.Core.Library.Miscelanea;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using sitScrum.Models.Authentication;
using sitScrum.Repositories;
using sitScrum.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace sitScrum.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthRepository _authRepository;

        public UserController(IAuthRepository authRepository)
        {
            this._authRepository = authRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(Credencial Usuario)
        {
            if (ModelState.IsValid)
            {
                pr_Eliminar_Sesiones();
                DatosUsuario vDatosUsuario;
                try
                {

                    if (this._authRepository.Login(Usuario.UserName.ToUpper(), Usuario.Password))
                    {

                        vDatosUsuario = this._authRepository.LeerDatosUsuario(Usuario.UserName.ToUpper(), Usuario.Password);
                        this.HttpContext.Session.SetObject("usrData", vDatosUsuario);

                        // *** Para crear el cookie de autenticacion
                        var claims = new[] { new Claim(ClaimTypes.Name, vDatosUsuario.Usuario),
                                            new Claim(ClaimTypes.Role, "roles") };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                        //  ***
                        Get_Permisos_Visualizacion(); // Lee Valores de las sesiones que se utilizan en el programa

                        IEnumerable<OpcionMenu> LstOptMenu = this._authRepository.LstOpcionesMenu(Usuario.UserName.ToUpper(), "S");

                        int vCantOpcionesMenus = (from x in LstOptMenu select x).Count();

                        if (vCantOpcionesMenus >= 0) // Verifica si el usuario tiene opciones de menú asignadas
                        {
                            this.HttpContext.Session.SetObject("LstOptMenu", LstOptMenu);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {

                            this.HttpContext.SignOutAsync();
                            ViewBag.CodMensajeBD = "ERR";
                            ViewBag.MensajeBD = "Usuario " + Usuario.UserName + " no tiene accesos a la aplicación";
                            return View(Usuario);
                        }
                    }
                    else
                    {
                        ViewBag.CodMensajeBD = "ERR";
                        ViewBag.MensajeBD = this._authRepository.getMensajeDB();
                        return View(Usuario);
                    }
                }
                catch (Exception exep)
                {
                    ViewBag.CodMensajeBD = "ERR";
                    ViewBag.MensajeBD = exep.Message;
                    return View(Usuario);
                }
            }
            else
            {
                ViewBag.CodMensajeBD = "ERR";
                ViewBag.MensajeBD = "Debe llenar toda la información";
                return View(Usuario);
            }

        }

        public void Get_Permisos_Visualizacion()
        {
            // para obtener la logica de permisos de visualizacion
        }
        public ActionResult LogOut()
        {
            if (HttpContext != null)
            {
                HttpContext.Session.Clear();
                HttpContext.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }


        public void pr_Eliminar_Sesiones()
        {
            if (HttpContext != null)
                HttpContext.Session.Clear();
        }
    }
}
