using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;

namespace SAC.Models
{
    public class BancoCuentaModelView
    {

        public int Id { get; set; }

        public string Codigo { get; set; }

        public int IdBanco { get; set; }

        public string BancoDescipcion { get; set; }

        public string BancoDescripcion { get; set; }

        public int IdImputacion { get; set; }

        public string CNombre { get; set; }

        public decimal Saldo { get; set; }
        public int NumeroCierre { get; set; }
        public System.DateTime Fecha { get; set; }
        public int IdMoneda { get; set; }

        public Nullable<bool> Activo { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        public Nullable<System.DateTime> UltimaModificacion { get; set; }

        public BancoModelView Banco { get; set; }


    }
}