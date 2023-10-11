using clbModels.Bitacora;
using clbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clbDataAccessInterface
{
    public interface IScrum_ActividadesRepository
    {
        string ErrorMessage { get; }
        List<Scrum_Actividades> Get(Scrum_Actividades model);
        List<ResponseDB> Set(Scrum_Actividades model);
        List<ResponseDB> Del(Scrum_Actividades model);
        List<ResponseDB> Upd(Scrum_Actividades model);
    }

}
