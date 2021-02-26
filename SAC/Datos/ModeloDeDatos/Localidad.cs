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
    
    public partial class Localidad
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Localidad()
        {
            this.ClienteDireccion = new HashSet<ClienteDireccion>();
            this.ClienteDireccion1 = new HashSet<ClienteDireccion>();
        }
    
        public int Id { get; set; }
        public Nullable<int> Codigo { get; set; }
        public string Nombre { get; set; }
        public string Altini { get; set; }
        public string AltFin { get; set; }
        public string CodigoProvincia { get; set; }
        public string NombreSucursal { get; set; }
        public Nullable<int> CodigoSucursal { get; set; }
        public Nullable<int> OfDistLocal { get; set; }
        public Nullable<int> codposchr { get; set; }
        public Nullable<int> IdPais { get; set; }
        public Nullable<int> IdProvincia { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClienteDireccion> ClienteDireccion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClienteDireccion> ClienteDireccion1 { get; set; }
        public virtual Pais Pais { get; set; }
        public virtual Provincia Provincia { get; set; }
    }
}
