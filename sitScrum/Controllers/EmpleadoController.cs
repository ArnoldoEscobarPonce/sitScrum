using IMSA.Core.Library.DataBase;
using IMSA.Core.Library.Library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NuGet.Protocol.Plugins;
using sitScrum.Models;
using sitScrum.Models.Authentication;
using sitScrum.util;
using System.Collections.Generic;
using System;
using System.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace sitScrum.Controllers
{
    public class EmpleadoController : Controller
    {

        public EmpleadoController(IConfiguration configuration, dbConnection dbConnection)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Saludar(int Id, string Nombre)
        {
            return View();
        }
    }
}
