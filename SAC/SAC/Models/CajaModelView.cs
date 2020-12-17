using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Negocio.Modelos;
using Datos.ModeloDeDatos;

namespace SAC.Models
{
    public class CajaModelView
    {




        public int Id { get; set; }
        public Nullable<int> IdTipoMovimiento { get; set; }

        [Required]       
        public string Concepto { get; set; }
       
        

        [Display(Name = "Fecha")]
        [Required]       
        public Nullable<System.DateTime> Fecha { get; set; }
       
        public string Tipo { get; set; }
        public string Saldo { get; set; }
        public Nullable<int> IdGrupoCaja { get; set; }


          public string Recibo { get; set; }

        [Display(Name = "Importe Pesos")]
               public Nullable<decimal> ImportePesos { get; set; }

        [Display(Name = "Importe Depósito")]
        public Nullable<decimal> ImporteDeposito { get; set; }

        public Nullable<int> IdCuentaBanco { get; set; }
        [Display(Name = "Importe Dolar")]
        public Nullable<decimal> ImporteDolar { get; set; }
        [Display(Name = "Importe Tarjeta")]
        public Nullable<decimal> ImporteTarjeta { get; set; }
        [Display(Name = "Importe Cheque")]
        public Nullable<decimal> ImporteCheque { get; set; }
        public Nullable<int> IdCajaSaldo { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }

        public virtual CajaTipoMovimiento CajaTipoMovimiento { get; set; }
        public virtual GrupoCaja GrupoCaja { get; set; }


        public  List<CajaModelView> ListaCaja { get; set; }

        //public decimal TotalPesos { get; set; }
        //public decimal TotalDolar { get; set; }
        //public decimal TotalDepositos { get; set; }
        //public decimal TotalTarjeta { get; set; }
        //public decimal TotalCheque { get; set; }


        //private int ImportePesos()
        //{
        //    throw new NotImplementedException();
        //}

        //public List<CajaModelView> ComprobanteDetalle { get; set; }

        //public decimal TotalPesos()
        //{
        //    return ComprobanteDetalle.Sum(x => x.ImportePesos());
        //}

        //public decimal TotalDolar()
        //{
        //    return ComprobanteDetalle.Sum(x => x.ImporteDolar());
        //}

        //public decimal TotalDeposito()
        //{
        //    return ComprobanteDetalle.Sum(x => x.ImporteDeposito());
        //}

        //public decimal TotalTarjeta()
        //{
        //    return ComprobanteDetalle.Sum(x => x.ImporteTarjeta());
        //}













    }
}