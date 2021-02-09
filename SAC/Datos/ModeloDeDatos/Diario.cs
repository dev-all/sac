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
    
    public partial class Diario
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public System.DateTime Fecha { get; set; }
        public int IdImputacion { get; set; }
        public string Descripcion { get; set; }
        public decimal Importe { get; set; }
        public string Titulo { get; set; }
        public string Periodo { get; set; }
        public string Tipo { get; set; }
        public string DescripcionMa { get; set; }
        public string Moneda { get; set; }
        public decimal Cotiza { get; set; }
        public Nullable<int> Balance { get; set; }
        public Nullable<int> Asiento { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }
    
        public virtual Imputacion Imputacion { get; set; }
    }
}
