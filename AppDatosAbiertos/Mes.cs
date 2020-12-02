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

    public partial class Mes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Mes()
        {
            this.RecursoClorogasSulfato = new HashSet<RecursoClorogasSulfato>();
            this.RecursoCResidual = new HashSet<RecursoCResidual>();
            this.RecursoNomina = new HashSet<RecursoNomina>();
            this.RecursoPrestServicios = new HashSet<RecursoPrestServicios>();
            this.RecursoProdAgua = new HashSet<RecursoProdAgua>();
        }
    
        public int IdMes { get; set; }
        [Display(Name = "Nombre del mes")]
        public string NombreMes { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RecursoClorogasSulfato> RecursoClorogasSulfato { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RecursoCResidual> RecursoCResidual { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RecursoNomina> RecursoNomina { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RecursoPrestServicios> RecursoPrestServicios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RecursoProdAgua> RecursoProdAgua { get; set; }
    }
}
