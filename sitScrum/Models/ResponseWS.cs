using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Web;

namespace sitTallerSap.Models
{
    public class ResponseWS<T>
    {
        public int Codigo { get; set; }

        public string Mensaje { get; set; }

        public T Resultado { get; set; }

        public ResponseWS()
        {

        }

        public ResponseWS(ResponseWsCode pCodigo, string pMensaje)
        {
            Codigo = (int)pCodigo;
            Mensaje = pMensaje;
        }

        public ResponseWS(ResponseWsCode pCodigo, string pMensaje, T pResultado)
        {
            Codigo = (int)pCodigo;
            Mensaje = pMensaje;
            Resultado = pResultado;
        }

    }
}