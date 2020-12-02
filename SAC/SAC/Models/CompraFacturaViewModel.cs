using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class CompraFacturaViewModel
    {

        public int Id { get; set; }
        public Nullable<int> IdTipoComprobante { get; set; }
        public Nullable<int> PuntoVenta { get; set; }
        public Nullable<int> NumeroFactura { get; set; }
        [Display(Name = "Proveedor ")]
        [Required(ErrorMessage = "Ops!, complete el campo Usuario.")]
        public int IdProveedor { get; set; }
        public Nullable<decimal> Total { get; set; }
        public decimal Saldo { get; set; }
        public System.DateTime Fecha { get; set; }
        public System.DateTime Vencimiento { get; set; }
        public decimal TotalDolares { get; set; }
        public decimal Cotiza { get; set; }
        public System.DateTime FechaPago { get; set; }
        public int Periodo { get; set; }
        public Nullable<decimal> Grupo { get; set; }
        public string Marca { get; set; }
        public string Pase { get; set; }
        public decimal Cotiza1 { get; set; }
        public string Concepto { get; set; }
        public int IdImputacion { get; set; }
        public int IdMoneda { get; set; }
        public decimal Parcial { get; set; }
        public int Recibo { get; set; }
        public string NumeroPago { get; set; }
        public int IdDiario { get; set; }
        public Nullable<int> Auxiliar { get; set; }
        public string AxiliarNumero { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }

        public ProveedorModelView Proveedor { get; set; }
        //public List<CompraFacturaPago> CompraFacturaPago { get; set; }
        //public Imputacion Imputacion { get; set; }

        public List<TipoMonedaModelView> TipoMonedas { get; set; }
        public List<TipoComprobanteModelView> TipoComprobante { get; set; }
    }
}