using IMSA.Core.Library;
using IMSA.Core.Library.Miscelanea;
using sitScrum.Models;
using sitScrum.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sitScrum.Repositories
{
    public interface IAuthRepository : ISystemConfig
    {
        bool Login(string username, string password);
        public DatosUsuario LeerDatosUsuario(string _UserName, string _pClave);
        public IEnumerable<OpcionMenu> LstOpcionesMenu(string _pUsuario, string _pVisible);
        public Empleado LeeInformacionEmpleado(string _pCod_Empleado);
        public string getCodigoDB();
        public string getMensajeDB();
    }
}
