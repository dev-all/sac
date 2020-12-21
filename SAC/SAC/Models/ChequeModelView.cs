using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;

namespace SAC.Models
{
    public class ChequeModelView
    {

        public int Id { get; set; }

        public int NumeroCheque { get; set; }

        public string IdBanco { get; set; }

        public double Fecha { get; set; }

        public string Clearing { get; set; }

        public decimal Importe { get; set; }

        public int IdCliente { get; set; }

        public string Descripcion { get; set; }

        public string NumeroRecibo { get; set; }

        public string FechaIngreso { get; set; }

        public string FechaEgreso { get; set; }

        public string Destino { get; set; }

        public string IdMoneda { get; set; }

        public string GrupoCaja { get; set; }

        public string IdFactura { get; set; }

        public string NumeroPago { get; set; }

        public string Registro { get; set; }

        public string Proveedor { get; set; }

        public Nullable<bool> Activo { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        public Nullable<System.DateTime> UltimaModificacion { get; set; }

        //propiedad agregada
        public bool seleccionado { get; set; }

        public TipoMoneda TipoMoneda { get; set; }

        //public string tipoMonedaDescripcion { get; set; }

    }
}