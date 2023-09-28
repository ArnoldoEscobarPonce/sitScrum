using IMSA.Core.Library.DataBase;
using IMSA.Core.Library.Library;
using IMSA.Core.Library.Miscelanea;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using sitScrum.Models;
using sitScrum.Models.Authentication;
using sitScrum.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace sitScrum.Repositories
{
    public class AuthRespository : UtilRepository, IAuthRepository
    {

        public AuthRespository(IConfiguration configuration,
                               IHttpContextAccessor httpContextAccessor,
                               IGuidGenerator_old guidGenerator)
        {
            _configuration = configuration;
            _httpContext = httpContextAccessor;
            _guidGenerator = guidGenerator;

        }

        public bool Login(string username, string password)
        {
            dbConnection vConection = GetConection();
            try
            {
                return vConection.Login(username, password);

            }
            catch (Exception exep)
            {

                mensajeBD[0] = "ERR";
                mensajeBD[1] = exep.Message;
                return false;
            }

        }

        public DatosUsuario LeerDatosUsuario(string _UserName, string _pClave)
        {
            dbConnection vConection = GetConection();
            vConection.User = _UserName;
            vConection.Password = _pClave;

            List<dbParametro> vParametros = new List<dbParametro>();

            DatosUsuario DatosUsuario = new DatosUsuario();
            DatosUsuario.NombreUsuario = "PENDIENTE";
            this.clearMensajeDBArry();

            DatosUsuario.Usuario = _UserName;
            DatosUsuario.Password = utilEncrypt.EncryptAES(_pClave, this._guidGenerator.getGuid());

            vParametros.Add(new dbParametro("p_Usuario", dbTipoDato.VARCHAR2, _UserName, dbTipoParametro.ENTRADA));
            vParametros.Add(new dbParametro("pResultadoCursor", dbTipoDato.REFCURSOR, "", dbTipoParametro.SALIDA));

            var vResultado = vConection.GetProcedureDs("Maestro.Pkg_Informacion_General.pr_Lee_Datos_Usuario", vParametros);
            if (vResultado != null)
            {
                DatosUsuario.NombreUsuario = vResultado.Tables[0].Rows[0]["Descripcion"].ToString();
                DatosUsuario.CodEmpleado = int.Parse(vResultado.Tables[0].Rows[0]["Cod_Empleado"].ToString());
            }

            return DatosUsuario;
        }


        public IEnumerable<OpcionMenu> LstOpcionesMenu(string _pUsuario, string _pVisible)
        {
            dbConnection vConection = GetConection();
            List<dbParametro> vParametros = new List<dbParametro>();

            vParametros.Add(new dbParametro("pUsuario", dbTipoDato.VARCHAR2, _pUsuario, dbTipoParametro.ENTRADA));
            vParametros.Add(new dbParametro("pCod_Sistema", dbTipoDato.INT32, this.CodSistema(), dbTipoParametro.ENTRADA));
            vParametros.Add(new dbParametro("pVisible", dbTipoDato.VARCHAR2, _pVisible, dbTipoParametro.ENTRADA));
            vParametros.Add(new dbParametro("pResultadoCursor", dbTipoDato.REFCURSOR, "", dbTipoParametro.SALIDA));

            var ds = vConection.GetProcedure<OpcionMenu>("Maestro.Pkg_Informacion_General.pr_Lee_Opciones_Menu", vParametros);

            return ds;

        }


        public Empleado LeeInformacionEmpleado(string _pCod_Empleado)
        {
            List<dbParametro> vParametros = new List<dbParametro>();
            vParametros.Add(new dbParametro("pCod_Empleado", dbTipoDato.INT32, _pCod_Empleado, dbTipoParametro.ENTRADA));
            vParametros.Add(new dbParametro("pResultadoCursor", dbTipoDato.REFCURSOR, "", dbTipoParametro.SALIDA));

            Empleado vEmpleado = new Empleado();
            try
            {
                var ds = GetConection().GetProcedure<Empleado>("Maestro.Pkg_Informacion_General.pr_Lee_Info_Empleado", vParametros);

                if (ds != null && ds.Count > 0)
                {
                    vEmpleado = ds[0];

                }

            }
            catch (Exception exep)
            {
                this.mensajeBD[0] = "ERR";
                this.mensajeBD[1] = utilErrorMsg.Get_Msg_Excepcion(exep);
            }
            return vEmpleado;
        }


    }
}
