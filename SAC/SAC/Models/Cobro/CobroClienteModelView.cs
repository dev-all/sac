using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;
using System.Web.Mvc;
using Negocio.Modelos;

namespace SAC.Models.Cobro
{
    public class CobroClienteModelView
    {
        
        [Display(Name = "Cliente")]
        public int IdCliente { get; set; }

        public ClienteModelView Cliente { get; set; }

        [Display(Name = "Cotización")]     
        public ValorCotizacionModel Cotizacion { get; set; }

        [Display(Name = "Moneda de Operación")]
        [Required]
        public Nullable<int> IdTipoMoneda { get; set; }

        public int Periodo { get; set; }
        public String Fecha { get; set; }

        public List<PagosFacturasModelView> CuentaCorriente { get; set; }
        public List<PagosFacturasModelView> ResumenPago { get; set; }
        
 
        public List<SelectListItem> SelectTipoMoneda { get; set; }
        public List<SelectListItem> SelectCuentasBancarias { get; set; }
        public List<SelectListItem> SelectTarjetas { get; set; }
        public List<SelectListItem> SelectPresupuestoActual { get; set; }
        public List<ChequeModelView> ListaChequesTerceros { get; set; }
        public List<ChequeraModelView> ListaChequesPropios { get; set; }
        public ChequeraModelView Chequera { get; set; }


        public DateTime UltimaModificacion { get; set; }


        public string ConceptoCobro { get; set; }
        public int NumeroRecibo { get; set; }
        public Nullable<int> IdCuentasBancarias { get; set; }
        public Nullable<int> IdTarjeta { get; set; }

        public string idChequesPropios { get; set; }
        public string idChequesTerceros { get; set; }
        public Nullable<int> IdPresupuesto { get; set; }
        public Nullable<int> IdRetencion { get; set; }

        public decimal montoEfectivo { get; set; }
        [Display(Name = " Monto cheques")]
        public decimal montoChequesSeleccionados { get; set; }
        public decimal montoTarjeta { get; set; }
        public decimal montoCuentaBancaria { get; set; }
        public decimal montoRetencion_ { get; set; }
        public decimal montoTotal { get; set; }

        //retenciones 
        public RetencionModelView Retencion { get; set; }
        public List<RetencionModelView> ListadoRetenciones { get; set; }
        public decimal TotalRetenciones { get; set; }


    }

}