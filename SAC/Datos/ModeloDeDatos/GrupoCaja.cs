//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Datos.ModeloDeDatos
{
    using System;
    using System.Collections.Generic;
    
    public partial class GrupoCaja
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GrupoCaja()
        {
            this.Caja = new HashSet<Caja>();
        }
    
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Concepto { get; set; }
        public Nullable<decimal> TotalPesos { get; set; }
        public Nullable<decimal> TotalDolares { get; set; }
        public Nullable<decimal> TotalCheques { get; set; }
        public Nullable<decimal> TotalTarjetas { get; set; }
        public Nullable<decimal> TotalDepositos { get; set; }
        public Nullable<decimal> ParcialPesos { get; set; }
        public Nullable<decimal> ParcialDolares { get; set; }
        public Nullable<decimal> ParcialCheques { get; set; }
        public Nullable<decimal> ParcialTarjetas { get; set; }
        public Nullable<decimal> ParcialDepositos { get; set; }
        public Nullable<int> IdImputacion { get; set; }
        public Nullable<bool> NoBorrar { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Caja> Caja { get; set; }
    }
}
