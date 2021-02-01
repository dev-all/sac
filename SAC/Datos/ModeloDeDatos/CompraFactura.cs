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
    
    public partial class CompraFactura
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CompraFactura()
        {
            this.CompraFacturaPago = new HashSet<CompraFacturaPago>();
            this.Retencion = new HashSet<Retencion>();
            this.TrackingFacturaPagoCompra = new HashSet<TrackingFacturaPagoCompra>();
            this.TrackingFacturaPagoCompra1 = new HashSet<TrackingFacturaPagoCompra>();
        }
    
        public int Id { get; set; }
        public int IdTipoComprobante { get; set; }
        public int PuntoVenta { get; set; }
        public int NumeroFactura { get; set; }
        public int IdProveedor { get; set; }
        public string IdTipoIva { get; set; }
        public string CAE { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<decimal> Saldo { get; set; }
        public System.DateTime Fecha { get; set; }
        public System.DateTime Vencimiento { get; set; }
        public Nullable<decimal> TotalDolares { get; set; }
        public decimal Cotizacion { get; set; }
        public Nullable<System.DateTime> FechaPago { get; set; }
        public Nullable<int> Periodo { get; set; }
        public Nullable<decimal> Grupo { get; set; }
        public string Marca { get; set; }
        public string Pase { get; set; }
        public Nullable<decimal> CotizacionDePago { get; set; }
        public string Concepto { get; set; }
        public Nullable<int> IdImputacion { get; set; }
        public Nullable<int> IdMoneda { get; set; }
        public Nullable<int> IdCompraIva { get; set; }
        public Nullable<decimal> Parcial { get; set; }
        public Nullable<int> Recibo { get; set; }
        public Nullable<int> NumeroPago { get; set; }
        public Nullable<int> IdCompraFacturaAplica { get; set; }
        public Nullable<int> Auxiliar { get; set; }
        public string AxiliarNumero { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }
    
        public virtual CompraIva CompraIva { get; set; }
        public virtual Imputacion Imputacion { get; set; }
        public virtual Proveedor Proveedor { get; set; }
        public virtual TipoComprobante TipoComprobante { get; set; }
        public virtual TipoMoneda TipoMoneda { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompraFacturaPago> CompraFacturaPago { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Retencion> Retencion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrackingFacturaPagoCompra> TrackingFacturaPagoCompra { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrackingFacturaPagoCompra> TrackingFacturaPagoCompra1 { get; set; }
    }
}
