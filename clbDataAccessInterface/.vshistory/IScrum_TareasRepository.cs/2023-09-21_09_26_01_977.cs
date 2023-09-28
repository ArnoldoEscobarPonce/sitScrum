
using clbModels;
using clbModels.Bitacora;

namespace clbDataAccessInterface
{
    public interface IScrum_TareasRepository
    {
        string ErrorMessage { get; }
        List<Scrum_Tareas> Get(Scrum_Tareas operacion);
        List<ResponseDB> Set(Scrum_Tareas operacion);
        List<ResponseDB> Del(Scrum_Tareas operacion);
        List<ResponseDB> Upd(Scrum_Tareas operacion);
    }
}