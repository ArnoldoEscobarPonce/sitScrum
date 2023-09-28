using clbDataAccessInterface;
using clbModels;
using clbModels.Authentication;
using clbModels.Bitacora;
using clbModels.ImsaConfig;
using IMSA.Core.Library.DataBase;
using IMSA.Core.Library.Library;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using IMSAUtil.Sessions;
using IMSAUtilInterface.GUID;

namespace clbDataAccess
{
    public class Scrum_TareasRepository : IScrum_TareasRepository
    {
        private readonly IConfiguration _configuration;
        private dbConnection _dbConnection = new dbConnection();
        private string errormessage;

        string IScrum_TareasRepository.ErrorMessage { get => errormessage; }

        public Scrum_TareasRepository(IConfiguration configuration, IOptions<Configuracion> imsaConfig, IHttpContextAccessor httpContextAccessor, IGuidGenerator guidGenerator)
        {
            var datoaUsuario = httpContextAccessor.HttpContext.Session.GetObject<DatosUsuario>("usrData");
            _configuration = configuration;
            _dbConnection.ConnectionString = configuration["ConnectionStrings:" + configuration["AppSettings:Ambiente"].ToString()].ToString();
            _dbConnection.User = datoaUsuario.Usuario;
            _dbConnection.Password = utilEncrypt.DecryptAES(datoaUsuario.Password, guidGenerator.getGuid());
            _dbConnection.EncriptedString = true;
        }

        public List<ResponseDB> Del(Scrum_Tareas model)
        {
            try
            {
                List<dbParametro> dbParametros = new List<dbParametro>() {
                new dbParametro("P_COD_TAREA",dbTipoDato.INT64,model.Cod_Tarea ,dbTipoParametro.ENTRADA),
                new dbParametro("P_RESULTADO",dbTipoDato.REFCURSOR,null,dbTipoParametro.SALIDA)
                };

                return _dbConnection.GetProcedure<ResponseDB>("BITACORA.PKG_SCRUM.PR_DEL_SCRUM_TAREAS", dbParametros);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.InnerException == null ? "" : ex.InnerException.Message, ex);
            }
        }

        public List<Scrum_Tareas> Get(Scrum_Tareas model)
        {
            try
            {
                List<dbParametro> dbParametros = new List<dbParametro>();
                dbParametros.Add(new dbParametro("P_COD_TAREA", dbTipoDato.INT64, model.Cod_Tarea, dbTipoParametro.ENTRADA));
                dbParametros.Add(new dbParametro("P_RESULTADO", dbTipoDato.REFCURSOR, null, dbTipoParametro.SALIDA));

                return _dbConnection.GetProcedure<Scrum_Tareas>(
                    "BITACORA.PKG_SCRUM.PR_GET_SCRUM_TAREAS", dbParametros);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.InnerException == null ? "" : ex?.InnerException?.Message, ex);
            }
        }

        public List<ResponseDB> Set(Scrum_Tareas model)
        {
            try
            {
                List<dbParametro> dbParametros = new List<dbParametro>() {
                new dbParametro("P_COD_ACTIVIDAD",dbTipoDato.INT64,model.Cod_Actividad ,dbTipoParametro.ENTRADA),
                new dbParametro("P_DESCRIPCION",dbTipoDato.VARCHAR2,model.Descripcion ,dbTipoParametro.ENTRADA),
                new dbParametro("P_ANOTACIONES",dbTipoDato.VARCHAR2,model.Anotaciones ,dbTipoParametro.ENTRADA),
                new dbParametro("P_COD_EMPLEADO_RESPONSABLE",dbTipoDato.INT64,model.Cod_Empleado_Responsable ,dbTipoParametro.ENTRADA),
                new dbParametro("P_FECHA_INICIO",dbTipoDato.DATE,model.Fecha_Inicio ,dbTipoParametro.ENTRADA),
                new dbParametro("P_FECHA_FIN_REAL",dbTipoDato.DATE,model.Fecha_Fin_Real ,dbTipoParametro.ENTRADA),
                new dbParametro("P_RESULTADO",dbTipoDato.REFCURSOR,null,dbTipoParametro.SALIDA)
                };

                return _dbConnection.GetProcedure<ResponseDB>("BITACORA.PKG_SCRUM.PR_SET_SCRUM_TAREAS", dbParametros);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.InnerException == null ? "" : ex.InnerException.Message, ex);
            }
        }

        public List<ResponseDB> Upd(Scrum_Tareas model)
        {
            try
            {
                List<dbParametro> dbParametros = new List<dbParametro>() {
                new dbParametro("P_COD_TAREA",dbTipoDato.INT64,model.Cod_Tarea ,dbTipoParametro.ENTRADA),
                new dbParametro("P_COD_ACTIVIDAD",dbTipoDato.INT64,model.Cod_Actividad ,dbTipoParametro.ENTRADA),
                new dbParametro("P_DESCRIPCION",dbTipoDato.VARCHAR2,model.Descripcion ,dbTipoParametro.ENTRADA),
                new dbParametro("P_ANOTACIONES",dbTipoDato.VARCHAR2,model.Anotaciones ,dbTipoParametro.ENTRADA),
                new dbParametro("P_COD_EMPLEADO_RESPONSABLE",dbTipoDato.INT64,model.Cod_Empleado_Responsable ,dbTipoParametro.ENTRADA),
                new dbParametro("P_FECHA_INICIO",dbTipoDato.DATE,model.Fecha_Inicio ,dbTipoParametro.ENTRADA),
                new dbParametro("P_FECHA_FIN_ESTIMADA",dbTipoDato.DATE,model.Fecha_Fin_Estimada ,dbTipoParametro.ENTRADA),
                new dbParametro("P_FECHA_FIN_REAL",dbTipoDato.DATE,model.Fecha_Fin_Real ,dbTipoParametro.ENTRADA),
                new dbParametro("P_RESULTADO",dbTipoDato.REFCURSOR,null,dbTipoParametro.SALIDA)
                };

                return _dbConnection.GetProcedure<ResponseDB>("BITACORA.PKG_SCRUM.PR_UPD_SCRUM_TAREAS", dbParametros);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.InnerException == null ? "" : ex.InnerException.Message, ex);
            }
        }
    }
}