using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sitScrum.Models.Authentication
{
    public class OpcionMenu
    {
        public Decimal Cod_Menu { get; set; }
        public Decimal? Cod_Menu_Padre { get; set; }
        public string Nombre_Opcion { get; set; }
        public string Visible { get; set; }
        public int Orden { get; set; }
        public string Controlador { get; set; }
        public string Metodo { get; set; }

        public string Parametros { get; set; }
    }
}