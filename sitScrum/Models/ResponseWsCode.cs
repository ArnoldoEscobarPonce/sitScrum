using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sitTallerSap.Models
{
    public enum ResponseWsCode
    {
        /// <summary>
        /// Error al consumir WS
        /// </summary>
        ERROR = 0,
        /// <summary>
        /// WS consumido de forma exitosa
        /// </summary>
        EXITO = 1
    }
}
