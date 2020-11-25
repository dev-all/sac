
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
    
public partial class Rubros
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Rubros()
    {

        this.SubRubro = new HashSet<SubRubro>();

    }


    public int Id { get; set; }

    public string Codigo { get; set; }

    public string Descripcion { get; set; }

    public Nullable<int> IdGrupoCuenta { get; set; }

    public Nullable<decimal> Total { get; set; }

    public Nullable<bool> Activo { get; set; }

    public Nullable<int> IdUsuario { get; set; }

    public Nullable<System.DateTime> UltimaModificacion { get; set; }



    public virtual GrupoCuenta GrupoCuenta { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<SubRubro> SubRubro { get; set; }

}

}
