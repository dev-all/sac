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
    
    public partial class Notificacion
    {
        public int Id { get; set; }
        public int IdPersona { get; set; }
        public Nullable<bool> EnvioEmail { get; set; }
        public string Obs { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaCreacion { get; set; }
    
        public virtual Persona Persona { get; set; }
    }
}
