using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class LocalidadModelView
    {


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



        public virtual Pais Pais { get; set; }

        public virtual Provincia Provincia { get; set; }





    }
}