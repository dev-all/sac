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
    
    public partial class BancoCuenta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BancoCuenta()
        {
            this.BancoCuentaBancaria = new HashSet<BancoCuentaBancaria>();
            this.Caja = new HashSet<Caja>();
            this.CompraFacturaPago = new HashSet<CompraFacturaPago>();
            this.Chequera = new HashSet<Chequera>();
        }
    
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Banco { get; set; }
        public string Descripcion { get; set; }
        public int IdImputacion { get; set; }
        public string CNombre { get; set; }
        public decimal Saldo { get; set; }
        public int Cierre { get; set; }
        public string Fecha { get; set; }
        public int IdMoneda { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BancoCuentaBancaria> BancoCuentaBancaria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Caja> Caja { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompraFacturaPago> CompraFacturaPago { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Chequera> Chequera { get; set; }
    }
}
