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

    public partial class RecursoClorogasSulfato
    {
        public int IdRecursoCGasSulfato { get; set; }
        public int IdConjunto { get; set; }
        [Display(Name = "A�o")]
        public int Anio { get; set; }
        public int IdMes { get; set; }
        [Display(Name = "Cloro Gas")]
        public decimal CCloroGas { get; set; }
        [Display(Name = "Sulfato")]
        public decimal CSulfato { get; set; }
    
        public virtual ConjuntodeDato ConjuntodeDato { get; set; }
        public virtual Mes Mes { get; set; }
    }
}
