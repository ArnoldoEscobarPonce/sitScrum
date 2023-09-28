using clbModels;
using clbModels.Bitacora;

namespace clbBusinessInterface
{
    public interface IScrum_TareasBO
    {
        string ErrorMessage { get; }
        public List<Scrum_Tareas> Get(Scrum_Tareas operacion);
        public List<ResponseDB> Set(Scrum_Tareas operacion);
        public List<ResponseDB> Del(Scrum_Tareas operacion);
        public List<ResponseDB> Upd(Scrum_Tareas operacion);
    }
}