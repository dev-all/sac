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
    
    public partial class CompraFacturaPago
    {
        public int Id { get; set; }
        public Nullable<int> IdFacturaCompra { get; set; }
        public Nullable<int> IdTipoPago { get; set; }
        public Nullable<int> IdCheque { get; set; }
        public Nullable<int> IdChequera { get; set; }
        public Nullable<int> IdTarjeta { get; set; }
        public Nullable<int> IdBancoCuenta { get; set; }
        public Nullable<decimal> Monto { get; set; }
        public string Observaciones { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }
    
        public virtual Tarjetas Tarjetas { get; set; }
        public virtual CompraFactura CompraFactura { get; set; }
    }
}
