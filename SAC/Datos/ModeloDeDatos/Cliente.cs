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
    
    public partial class Cliente
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public Nullable<int> IdProveedor { get; set; }
        public string Telefono { get; set; }
        public string Fax { get; set; }
        public Nullable<decimal> Iva { get; set; }
        public string Cuit { get; set; }
        public Nullable<int> DiasFactura { get; set; }
        public Nullable<int> IdImputacion { get; set; }
        public string Observaciones { get; set; }
        public Nullable<decimal> SaldoCuentaCorriente { get; set; }
        public Nullable<int> IdPais { get; set; }
        public Nullable<bool> Especial { get; set; }
        public string CodigoPostal { get; set; }
        public string Pie { get; set; }
        public Nullable<int> CanFac { get; set; }
        public Nullable<int> IdIdioma { get; set; }
        public Nullable<int> IdTipoCliente { get; set; }
        public string Locala { get; set; }
        public Nullable<int> IdMoneda { get; set; }
        public Nullable<int> IdGrupoPresupuesto { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }
    
        public virtual GrupoPresupuesto GrupoPresupuesto { get; set; }
        public virtual TipoCliente TipoCliente { get; set; }
    }
}
