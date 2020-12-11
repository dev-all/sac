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
        public decimal? NetoTotalD { get; set; }
        public decimal? NetoTotalC { get; set; }
        public decimal? NetoTotalT { get; set; }
        public decimal? NetoTotalB { get; set; }
        public decimal? NetoParcialP { get; set; }
        public decimal? NetoParcialD { get; set; }
        public decimal? NetoParcialC { get; set; }
        public decimal? NetoParcialT { get; set; }
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