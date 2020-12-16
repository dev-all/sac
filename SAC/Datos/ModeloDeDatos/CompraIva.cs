
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
    
public partial class CompraIva
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public CompraIva()
    {

        this.CompraFactura = new HashSet<CompraFactura>();

    }


    public int Id { get; set; }

    public Nullable<int> CodigoDiario { get; set; }

    public decimal NetoGravado { get; set; }

    public decimal NetoNoGravado { get; set; }

    public decimal SubTotal { get; set; }

    public decimal TotalIva { get; set; }

    public decimal TotalPercepciones { get; set; }

    public decimal Total { get; set; }

    public Nullable<decimal> Importe25 { get; set; }

    public Nullable<decimal> Importe5 { get; set; }

    public Nullable<decimal> Importe105 { get; set; }

    public Nullable<decimal> Importe21 { get; set; }

    public Nullable<decimal> Importe27 { get; set; }

    public Nullable<decimal> Iva25 { get; set; }

    public Nullable<decimal> Iva5 { get; set; }

    public Nullable<decimal> Iva105 { get; set; }

    public Nullable<decimal> Iva21 { get; set; }

    public Nullable<decimal> Iva27 { get; set; }

    public Nullable<decimal> PercepcionIva { get; set; }

    public Nullable<decimal> PercepcionIB { get; set; }

    public Nullable<decimal> PercepcionProvincia { get; set; }

    public Nullable<decimal> PercepcionImporteIva { get; set; }

    public Nullable<decimal> PercepcionImporteIB { get; set; }

    public Nullable<decimal> PercepcionImporteProvincia { get; set; }

    public Nullable<decimal> OtrosImpuestos { get; set; }

    public Nullable<bool> Activo { get; set; }

    public Nullable<int> Idusuario { get; set; }

    public Nullable<System.DateTime> UltimaModificacion { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<CompraFactura> CompraFactura { get; set; }

}

}
