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
    
    public partial class Proveedor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Proveedor()
        {
            this.CompraFactura = new HashSet<CompraFactura>();
        }
    
        public int Id { get; set; }
        public string Cuit { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public Nullable<int> IdPais { get; set; }
        public Nullable<int> IdProvincia { get; set; }
        public string Telefono { get; set; }
        public Nullable<int> IdTipoIva { get; set; }
        public Nullable<int> DiasFactura { get; set; }
        public Nullable<int> IdImputacionProveedor { get; set; }
        public string Observaciones { get; set; }
        public string Email { get; set; }
        public Nullable<int> IdCodigoPostal { get; set; }
        public Nullable<int> IdTipoProveedor { get; set; }
        public Nullable<int> IdImputacionFactura { get; set; }
        public Nullable<int> IdMonedaFactura { get; set; }
        public string DescripcionFactura { get; set; }
        public Nullable<int> IdTipoMoneda { get; set; }
        public Nullable<int> IdPresupuesto { get; set; }
        public Nullable<int> UltimoPuntoVenta { get; set; }
        public bool Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompraFactura> CompraFactura { get; set; }
        public virtual Pais Pais { get; set; }
        public virtual Provincia Provincia { get; set; }
        public virtual TipoIva TipoIva { get; set; }
        public virtual TipoMoneda TipoMoneda { get; set; }
        public virtual TipoProveedor TipoProveedor { get; set; }
        public virtual Imputacion Imputacion { get; set; }
        public virtual Imputacion Imputacion1 { get; set; }
        public virtual Presupuesto Presupuesto { get; set; }
    }
}
