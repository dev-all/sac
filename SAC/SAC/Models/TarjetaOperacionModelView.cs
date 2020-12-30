using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;

namespace SAC.Models
{
    public class TarjetaOperacionModelView
    {

        public int Id { get; set; }
        public int IdTarjeta { get; set; }
        public int IdGrupoCaja { get; set; }
        public string Descripcion { get; set; }
        public Nullable<bool> Conciliacion { get; set; }
        public string NumeroPago { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }

       // public ICollection<CompraFacturaPago> CompraFacturaPago { get; set; }
        public TarjetaModelView Tarjetas { get; set; }
    }
}