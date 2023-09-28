using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clbModels.Authentication
{
    public class DatosUsuario
    {
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string NombreUsuario { get; set; }
        public int CodEmpleado { get; set; }
        public string Perfil { get; set; }

        public DatosUsuario() { }
        public DatosUsuario(string pNombreUsuario, int pCodEmpleado)
        {
            NombreUsuario = pNombreUsuario;
            CodEmpleado = pCodEmpleado;
        }
    }
}
