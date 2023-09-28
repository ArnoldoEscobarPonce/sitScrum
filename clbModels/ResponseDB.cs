using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clbModels
{
    public class ResponseDB
    {
        public double cod_error { get; set; }
        public string existe_error { get; set; }
        public string mensaje { get; set; }
        public double numero_registro { get; set; }
    }
}
