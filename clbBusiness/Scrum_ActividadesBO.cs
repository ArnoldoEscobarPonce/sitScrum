using clbDataAccessInterface;
using clbModels.Bitacora;
using clbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clbBusinessInterface;

namespace clbBusiness
{
    public class Scrum_ActividadesBO : IScrum_ActividadesBO
    {
        
        private readonly IScrum_ActividadesRepository _OperacionRepository;
        private string errormessage;

        public Scrum_ActividadesBO(IScrum_ActividadesRepository SectorRepository)
        {
            _OperacionRepository = SectorRepository;
        }

        public string ErrorMessage => throw new NotImplementedException();

        public List<Scrum_Actividades> Get(Scrum_Actividades operacion)
        {
            var ResultService = _OperacionRepository.Get(operacion);
            errormessage = _OperacionRepository.ErrorMessage;

            return ResultService.ToList();
        }

        public List<ResponseDB> Set(Scrum_Actividades operacion)
        {
            var ResultService = _OperacionRepository.Set(operacion);
            errormessage = _OperacionRepository.ErrorMessage;

            return ResultService.ToList();
        }

        public List<ResponseDB> Upd(Scrum_Actividades operacion)
        {
            var ResultService = _OperacionRepository.Upd(operacion);
            errormessage = _OperacionRepository.ErrorMessage;

            return ResultService.ToList();
        }

        public List<ResponseDB> Del(Scrum_Actividades operacion)
        {
            var ResultService = _OperacionRepository.Del(operacion);
            errormessage = _OperacionRepository.ErrorMessage;

            return ResultService.ToList();
        }


    }
}
