using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class ChequeraModelView
    {

        public int Id { get; set; }

        public int NumeroCheque { get; set; }

        public string Banco { get; set; }

        public double Fecha { get; set; }

        public double Importes { get; set; }

        public string IdProveedor { get; set; }

        public string Descripcion { get; set; }

        public string NumeroRecibo { get; set; }

        public double FechaIngreso { get; set; }

        public string FechaEgreso { get; set; }

        public string Destino { get; set; }

        public string IdMoneda { get; set; }

        public string GrupoCaja { get; set; }

        public int IdFactura { get; set; }

        public int NumeroPago { get; set; }

        public string Registro { get; set; }

        public Nullable<bool> Usado { get; set; }

        public Nullable<bool> Activo { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        public Nullable<System.DateTime> UltimaModificacion { get; set; }


        //propiedad agregada
        public bool seleccionado { get; set; }

        public string tipoMonedaDescripcion { get; set; }
    }
}