//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoWeb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Departamentos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Departamentos()
        {
            this.Empleados = new HashSet<Empleados>();
        }
    
        public int IdDepartamento { get; set; }
        [Display(Name = "Departamento")]
        public string Nombre { get; set; }
        [Display(Name = "Ubicacion")]
        public string UbicacionFisica { get; set; }
        [Display(Name = "Encargado")]
        public string ResponsableArea { get; set; }
        public bool Estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Empleados> Empleados { get; set; }
    }
}