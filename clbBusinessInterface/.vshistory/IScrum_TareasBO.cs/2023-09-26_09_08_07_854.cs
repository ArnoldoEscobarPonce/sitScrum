using clbModels;

namespace clbBusinessInterface
{
    public interface IScrum_TareasBO
    {
        string ErrorMessage { get; }
        public List<Inc_Com_M_Operacion> Get(Inc_Com_M_Operacion operacion);
        public List<ResponseDB> Set(Inc_Com_M_Operacion operacion);
        public List<ResponseDB> Del(Inc_Com_M_Operacion operacion);
        public List<ResponseDB> Upd(Inc_Com_M_Operacion operacion);
    }
}