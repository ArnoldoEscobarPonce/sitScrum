using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System;

namespace sitScrum.Models
{
    public class Inc_Com_M_Operacion
    {
        [DataMember]
        [Display(Name = "Operacion")]
        public double? Cod_Operacion { get; set; }
        [DataMember]
        [Display(Name = "Descripcion")]
        public string? Descripcion { get; set; }
        [DataMember]
        [Display(Name = "Descripcion")]
        public string _Descripcion { get { return string.Format("[{0}] - {1}", Cod_Operacion, Descripcion); } set { } }
        [DataMember]
        [Display(Name = "Motor Encendido")]
        public string? Motor_Encendido { get; set; }
        [DataMember]
        [Display(Name = "Desplazamiento")]
        public string? Desplazamiento { get; set; }
        [DataMember]
        [Display(Name = "Estado")]
        public string? Estado { get; set; }
        [DataMember]
        [Display(Name = "Usuario_Grabo")]
        public string? Usuario_Grabo { get; set; }
        [DataMember]
        [Display(Name = "Fecha_Hora_Grabo")]
        public DateTime? Fecha_Hora_Grabo { get; set; }
        [DataMember]
        [Display(Name = "Terminal_Grabo")]
        public string? Terminal_Grabo { get; set; }
        [DataMember]
        [Display(Name = "Usuario_Modifico")]
        public string? Usuario_Modifico { get; set; }
        [DataMember]
        [Display(Name = "Fecha_Hora_Modifico")]
        public DateTime? Fecha_Hora_Modifico { get; set; }
        [DataMember]
        [Display(Name = "Terminal_Modifico")]
        public string? Terminal_Modifico { get; set; }
        /// <summary> I:Insert, U:Update </summary>
        public string? Accion { get; set; }
    }
}
