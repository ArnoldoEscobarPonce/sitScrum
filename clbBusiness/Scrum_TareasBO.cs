using clbBusinessInterface;
using clbDataAccessInterface;
using clbModels;
using clbModels.Bitacora;

namespace clbBusiness
{
    public class Scrum_TareasBO : IScrum_TareasBO
    {
        private readonly IScrum_TareasRepository _OperacionRepository;
        private string errormessage;

        public Scrum_TareasBO(IScrum_TareasRepository SectorRepository)
        {
            _OperacionRepository = SectorRepository;
        }




        public string ErrorMessage => throw new NotImplementedException();

        public List<Scrum_Tareas> Get(Scrum_Tareas operacion)
        {
            var ResultService = _OperacionRepository.Get(operacion);
            errormessage = _OperacionRepository.ErrorMessage;

            return ResultService.ToList();
        }

        public List<ResponseDB> Set(Scrum_Tareas operacion)
        {
            var ResultService = _OperacionRepository.Set(operacion);
            errormessage = _OperacionRepository.ErrorMessage;

            return ResultService.ToList();
        }

        public List<ResponseDB> Upd(Scrum_Tareas operacion)
        {
            var ResultService = _OperacionRepository.Upd(operacion);
            errormessage = _OperacionRepository.ErrorMessage;

            return ResultService.ToList();
        }

        public List<ResponseDB> Del(Scrum_Tareas operacion)
        {
            var ResultService = _OperacionRepository.Del(operacion);
            errormessage = _OperacionRepository.ErrorMessage;

            return ResultService.ToList();
        }
    }
}