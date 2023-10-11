using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace clbModels.Bitacora
{

    /// <summary>
    /// BITACORA.SCRUM_ACTIVIDADES para definicion de actividades del proyecto
    /// Tabla que contien las Actividades por Proyecto
    /// </summary>
    /// 
    [Serializable]
    [DataContract]

    public class Scrum_Actividades
    {

            [DataMember]
            [Display(Name = "Actividad")]
            public double? Cod_Actividad { get; set; }
            [DataMember]
            [Display(Name = "Proyecto")]
            public double? Cod_Proyecto { get; set; }
            [DataMember]
            [Display(Name = "Descripcion")]
            public string? Descripcion { get; set; }
            [DataMember]
            [Display(Name = "Anotaciones")]
            public string? Anotaciones { get; set; }
            [DataMember]
            [Display(Name = "Fecha Inicio")]
            public DateTime? Fecha_Inicio { get; set; }
            [DataMember]
            [Display(Name = "Fecha Fin")]
            public DateTime? Fecha_Fin { get; set; }
            [DataMember]
            [Display(Name = "Estado")]
            public string? Cod_Estado { get; set; }
            [DataMember]
            [Display(Name = "Grabo")]
            public string? Usuario_Grabo { get; set; }
            [DataMember]
            [Display(Name = "Fecha Grabo")]
            public DateTime? Fecha_Hora_Grabo { get; set; }
            [DataMember]
            [Display(Name = "Terminal Grabo")]
            public string? Terminal_Grabo { get; set; }
            [DataMember]
            [Display(Name = "Modifico")]
            public string? Usuario_Modifico { get; set; }
            [DataMember]
            [Display(Name = "Fecha Modifico")]
            public DateTime? Fecha_Hora_Modifico { get; set; }
            [DataMember]
            [Display(Name = "Terminal Modifico")]
            public string? Terminal_Modifico { get; set; }

            public string Accion { get; set; }

    }

}
