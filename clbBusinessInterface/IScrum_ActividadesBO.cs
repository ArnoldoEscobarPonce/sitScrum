using clbModels.Bitacora;
using clbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clbBusinessInterface
{
    public interface IScrum_ActividadesBO
    {
        string ErrorMessage { get; }
        public List<Scrum_Actividades> Get(Scrum_Actividades operacion);
        public List<ResponseDB> Set(Scrum_Actividades operacion);
        public List<ResponseDB> Del(Scrum_Actividades operacion);
        public List<ResponseDB> Upd(Scrum_Actividades operacion);
    }

}
