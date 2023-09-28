using IMSA.Core.Library.Library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NuGet.Protocol.Plugins;
using sitScrum.Models;
using sitScrum.Models.Authentication;
using sitScrum.util;
using System.Configuration;

namespace sitScrum.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly IConfiguration _configuration;
        private dbConnection _dbConnection = new dbConnection();

        public EmpleadoController(IConfiguration configuration, dbConnection dbConnection)
        {

            _dbConnection = dbConnection;

            _configuration = configuration;
            _dbConnection.ConnectionString = configuration["ConnectionStrings:" + configuration["AppSettings:Ambiente"].ToString()].ToString();
            _dbConnection.User = "ponce";
            _dbConnection.Password = "aponce1";
            _dbConnection.EncriptedString = true;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Saludar(int Id, string Nombre)
        {


            return View(new Persona() { Id = Id, Nombre = Nombre  });
        }
    }
}
