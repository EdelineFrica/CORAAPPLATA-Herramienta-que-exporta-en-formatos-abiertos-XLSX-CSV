//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppDatosAbiertos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class RecursoPrestServicios
    {
        public int IdRecursoPServicios { get; set; }
        public int IdConjunto { get; set; }
        [Display(Name = "A�o")]
        public int Anio { get; set; }
        public int IdMes { get; set; }
        [Display(Name = "Acuerdos de pago")]
        public int AcuerdoPago { get; set; }
        [Display(Name = "Nuevos usuarios")]
        public int NuevosUsuarios { get; set; }
        public int Reconexiones { get; set; }
    
        public virtual ConjuntodeDato ConjuntodeDato { get; set; }
        public virtual Mes Mes { get; set; }
    }
}
