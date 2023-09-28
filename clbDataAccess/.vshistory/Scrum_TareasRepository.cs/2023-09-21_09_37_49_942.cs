using clbDataAccessInterface;
using clbModels;
using clbModels.Bitacora;
using clbModels.ImsaConfig;
using IMSA.Core.Library.Library;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

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

        public List<ResponseDB> Del(Scrum_Tareas operacion)
        {
            throw new NotImplementedException();
        }

        public List<Scrum_Tareas> Get(Scrum_Tareas operacion)
        {
            throw new NotImplementedException();
        }

        public List<ResponseDB> Set(Scrum_Tareas operacion)
        {
            throw new NotImplementedException();
        }

        public List<ResponseDB> Upd(Scrum_Tareas operacion)
        {
            throw new NotImplementedException();
        }
    }
}