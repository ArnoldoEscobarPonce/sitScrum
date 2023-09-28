using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sitScrum.Models
{
    public class Empleado
    {
        public int Cod_Empleado { get; set; }
        public string Nombre_Empleado { get; set; }

        public byte[] Foto_Empleado { get; set; }
        public string MsgError { get; set; }

        [Display(Name = "Foto:")]
        public string Foto
        {
            get
            {
                byte[] ImgByteFoto = Foto_Empleado;
                if (ImgByteFoto != null)
                {
                    string strBase64 = Convert.ToBase64String(Foto_Empleado);
                    return "data:Image/tiff;base64," + strBase64;
                }
                else
                {
                    return null;
                }

            }
        }

        public string Tipo { get; set; }

    }
}