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
    
    public partial class AccionPorRol
    {
        public int idRolPorAccion { get; set; }
        public int idRol { get; set; }
        public int idAccion { get; set; }
    
        public virtual Accion Accion { get; set; }
        public virtual Rol Rol { get; set; }
    }
}
