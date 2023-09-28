using IMSA.Core.Library.Library;
using IMSA.Core.Library.Miscelanea;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using sitScrum.Models.Authentication;
using sitScrum.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace sitScrum.Repositories
{
    public class UtilRepository : ISystemConfig
    {
        protected dbConnection ConectionBD;
        protected IConfiguration _configuration;
        protected IHttpContextAccessor _httpContext;
        protected IGuidGenerator _guidGenerator;
        protected string[] mensajeBD = { "", "" };

        public UtilRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public UtilRepository()
        {

        }

        public string getClaimValue(String pName)
        {
            if (this._httpContext != null)
            {
                var identity = this._httpContext.HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    var valor = claims.ToList().Find(x => x.Type.Equals(pName));
                    if (valor != null)
                    {
                        return valor.Value;
                    }
                }

            }
            return null;
        }


        public dbConnection GetConection()
        {


            if (ConectionBD == null)
            {
                ConectionBD = new dbConnection();
                ConectionBD.User = getUsuario();
                ConectionBD.Password = getUsuarioBDSecret();
                ConectionBD.ConnectionString = GetConectionString();
                ConectionBD.EncriptedString = true;
            }
            return ConectionBD;
        }

        public dbConnection GetConection(string pUser, String pPassword)
        {

            if (ConectionBD == null)
            {
                ConectionBD = new dbConnection();
                ConectionBD.User = pUser;
                ConectionBD.Password = pPassword;
                ConectionBD.ConnectionString = GetConectionString();
                ConectionBD.EncriptedString = true;
            }

            return ConectionBD;
        }

        public string GetConectionString()
        {

            return _configuration.GetSection("ConnectionStrings").GetSection(this.Ambiente()).Value;
        }


        public string GetSecretKey()
        {
            var vKeyAzure = this._configuration[SecretName()];

            if (!string.IsNullOrEmpty(vKeyAzure)) return vKeyAzure;
            else
            {
                var vKeyDapi = utilEncrypt.DecoderBase64(this._configuration.GetSection("AppSettings:token").Value);
                return vKeyDapi;
            }

        }

        public int CodSistema()
        {
            return Convert.ToInt32(this._configuration.GetSection("AppSettings:CodSistema").Value);
        }

        public string Ambiente()
        {
            return this._configuration.GetSection("AppSettings:Ambiente").Value;
        }

        public string SecretName()
        {
            return this._configuration.GetSection("AppSettings:SecretName").Value;
        }

        public string getUsuario()
        {
            var vDatos = this._httpContext.HttpContext.Session.GetObject<DatosUsuario>("usrData");
            if (vDatos != null)
            {
                return vDatos.Usuario;
            }
            return "";
        }

        public string getUsuarioBDSecret()
        {
            var vDatos = this._httpContext.HttpContext.Session.GetObject<DatosUsuario>("usrData");
            if (vDatos != null)
            {
                return utilEncrypt.DecryptAES(vDatos.Password, _guidGenerator.getGuid());
            }
            return "";
        }

        public string getFormatoFecha()
        {
            try
            {
                var Formato = this._configuration.GetSection("AppSettings:FormatoFecha").Value;
                if (string.IsNullOrEmpty(Formato)) return "yyyy-MM-dd HH:mm";
                else return Formato;
            }
            catch
            {
                return "yyyy-MM-dd HH:mm";
            }

        }

        public string[] getMensajeDBArry()
        {
            return mensajeBD;
        }

        public void clearMensajeDBArry()
        {
            this.mensajeBD[0] = "";
            this.mensajeBD[1] = "";
        }

        public string getCodigoDB()
        {
            return this.mensajeBD[0];
        }
        public string getMensajeDB()
        {
            return this.mensajeBD[1];
        }

    }
}
