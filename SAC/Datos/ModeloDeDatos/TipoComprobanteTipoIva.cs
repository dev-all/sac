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
    
    public partial class TipoComprobanteTipoIva
    {
        public int Id { get; set; }
        public int IdTipoIva { get; set; }
        public Nullable<int> IdTipoComprobante { get; set; }
    
        public virtual TipoIva TipoIva { get; set; }
        public virtual TipoComprobante TipoComprobante { get; set; }
    }
}
