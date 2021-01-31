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
    
    public partial class Retencion
    {
        public int Id { get; set; }
        public Nullable<int> NroPago { get; set; }
        public Nullable<int> IdCodigoProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<decimal> Importe { get; set; }
        public Nullable<int> NroRecibo { get; set; }
        public Nullable<int> IdProvincia { get; set; }
        public Nullable<int> IdCompraFactura { get; set; }
        public Nullable<int> IdTipoRetencion { get; set; }
        public Nullable<int> Periodo { get; set; }
        public string Actividad { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> Idusuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }
    
        public virtual Provincia Provincia { get; set; }
        public virtual TipoRetencion TipoRetencion { get; set; }
        public virtual CompraFactura CompraFactura { get; set; }
    }
}
