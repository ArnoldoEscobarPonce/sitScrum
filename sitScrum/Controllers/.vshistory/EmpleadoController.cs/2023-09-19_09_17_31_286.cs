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
    [Authorize]
    public class EmpleadoController : MasterController
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
            try
            {
                List<dbParametro> dbParametros = new List<dbParametro>();

                dbParametros.Add(new dbParametro("P_COD_OPERACION", dbTipoDato.INT64, null, dbTipoParametro.ENTRADA));
                dbParametros.Add(new dbParametro("P_RESULTADO", dbTipoDato.REFCURSOR, null, dbTipoParametro.SALIDA));

                var vResult = _dbConnection.GetProcedure<Inc_Com_M_Operacion>("INTEGRACOM.PKG_INTEGRACION_IMSA_FLOW.PR_GET_OPERACION", dbParametros);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.InnerException == null ? "" : ex?.InnerException?.Message, ex);
            }

            return View(new Persona() { Id = Id, Nombre = Nombre  });
        }
    }
}
