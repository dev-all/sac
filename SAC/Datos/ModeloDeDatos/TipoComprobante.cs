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
    
    public partial class TipoComprobante
    {
        public int Id { get; set; }
        public string Denominacion { get; set; }
        public string Abreviatura { get; set; }
        public Nullable<int> Numero { get; set; }
        public Nullable<int> PuntoVenta { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public System.DateTime UltimaModificacion { get; set; }
    }
}
