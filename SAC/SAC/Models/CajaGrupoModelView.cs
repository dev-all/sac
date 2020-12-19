using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Negocio.Modelos;
using Datos.ModeloDeDatos;

namespace SAC.Models
{
    public class CajaGrupoModelView
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Concepto { get; set; }
        public decimal? NetoTotalP { get; set; }
        [Display(Name = "Neto Total Pesos")]
        public decimal? NetoTotalD { get; set; }
        [Display(Name = "Neto Total Dolarés")]
        public decimal? NetoTotalC { get; set; }
        [Display(Name = "Neto Total Cheque")]
        public decimal? NetoTotalT { get; set; }
        [Display(Name = "Neto Total Tarjeta")]
        public decimal? NetoTotalB { get; set; }
        [Display(Name = "Neto Total Pesos")]
        public decimal? NetoParcialP { get; set; }
        [Display(Name = "Neto Parcial Pesos")]
        public decimal? NetoParcialD { get; set; }
        [Display(Name = "Neto Parcial Dolarés")]
        public decimal? NetoParcialC { get; set; }
        [Display(Name = "Neto Parcial Cheques")]
        public decimal? NetoParcialT { get; set; }
        [Display(Name = "Neto Parcial Tarjeta")]
        public decimal? NParcialB { get; set; }
        public string Ltraa { get; set; }
        public string Ltram { get; set; }
        public Nullable<int> IdImputacion { get; set; }
        public string Especial { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }


    }
}