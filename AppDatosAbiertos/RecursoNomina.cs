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
    
    public partial class RecursoNomina
    {
        public int IdRecNomina { get; set; }
        public int IdConjunto { get; set; }
        public int Codigo { get; set; }
        public string NombreApellido { get; set; }
        public string Cargo { get; set; }
        public int Salario { get; set; }
        public int IdMes { get; set; }
        public int anio { get; set; }
    
        public virtual ConjuntodeDato ConjuntodeDato { get; set; }
        public virtual Mes Mes { get; set; }
    }
}
