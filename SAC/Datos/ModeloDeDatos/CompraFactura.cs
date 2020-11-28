
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

    }


    public int Id { get; set; }

    public Nullable<int> IdTipoComprobante { get; set; }

    public Nullable<int> PuntoVenta { get; set; }

    public Nullable<int> NumeroFactura { get; set; }

    public Nullable<int> IdProveedor { get; set; }

    public Nullable<decimal> Total { get; set; }

    public decimal Saldo { get; set; }

    public System.DateTime Fecha { get; set; }

    public System.DateTime Vencimiento { get; set; }

    public decimal TotalDolares { get; set; }

    public decimal Cotiza { get; set; }

    public System.DateTime FechaPago { get; set; }

    public int Periodo { get; set; }

    public Nullable<decimal> Grupo { get; set; }

    public string Marca { get; set; }

    public string Pase { get; set; }

    public decimal Cotiza1 { get; set; }

    public string Concepto { get; set; }

    public int IdImputacion { get; set; }

    public int IdMoneda { get; set; }

    public decimal Parcial { get; set; }

    public int Recibo { get; set; }

    public string NumeroPago { get; set; }

    public int IdDiario { get; set; }

    public Nullable<int> Auxiliar { get; set; }

    public string AxiliarNumero { get; set; }

    public Nullable<bool> Activo { get; set; }

    public Nullable<int> IdUsuario { get; set; }

    public Nullable<System.DateTime> UltimaModificacion { get; set; }



    public virtual Proveedor Proveedor { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<CompraFacturaPago> CompraFacturaPago { get; set; }

    public virtual Imputacion Imputacion { get; set; }

}

}
