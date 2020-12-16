using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;

namespace SAC.Models
{
    public class BancoCuentaBancariaModelView
    {

        public int Id { get; set; }

        public int NumeroCheque { get; set; }

        public int IdBanco { get; set; }

        public string BancoDescripcion { get; set; }

        public string CuentaDescripcion { get; set; }

        public double DFecha { get; set; }

        public double DFechaD { get; set; }

        public string NClearing { get; set; }

        public double DFechaEf { get; set; }

        public double NImporte { get; set; }

        public string IdCliente { get; set; }

        public string LCondicion { get; set; }

        public string CGrupoCaje { get; set; }

        public double DIngreso { get; set; }

        public string IdImputacion { get; set; }

        public string NumeroRecibo { get; set; }

        public string Factura { get; set; }

        public string NumeroPago { get; set; }

        public string Registro { get; set; }

        public Nullable<bool> Activo { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        public Nullable<System.DateTime> UltimaModificacion { get; set; }


    }
}