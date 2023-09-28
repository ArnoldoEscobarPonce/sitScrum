using clbDataAccessInterface;
using clbModels;
using clbModels.Bitacora;
using IMSA.Core.Library.Library;
using Microsoft.Extensions.Configuration;

namespace clbDataAccess
{
    public class Scrum_TareasRepository : IScrum_TareasRepository
    {
        private readonly IConfiguration _configuration;
        private dbConnection _dbConnection = new dbConnection();
        private string errormessage;

        public string ErrorMessage => throw new NotImplementedException();

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