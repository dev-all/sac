using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class CompraFacturaViewModel
    {
        public CompraFacturaViewModel()
        {
            Cotizacion = 1;
        }

        public int Id { get; set; }
        [Display(Name = "Tipo Comprobante")]
        public int IdTipoComprobante { get; set; }
        [Display(Name = "Punto Venta ")]
        public int PuntoVenta { get; set; }
        [Display(Name = "Número Factura ")]
        public int NumeroFactura { get; set; }       
        [Display(Name = "Proveedor")]
        public int IdProveedor { get; set; }   
        public string IdTipoIva { get; set; }
        public string CAE { get; set; }
        public decimal Total { get; set; }
        public decimal Saldo { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Vencimiento { get; set; }
        public decimal TotalDolares { get; set; }
        public decimal Cotizacion { get; set; }
        public DateTime FechaPago { get; set; }
        public int Periodo { get; set; }
        public decimal Grupo { get; set; }
        public string Marca { get; set; }
        public string Pase { get; set; }
        public decimal CotizacionDePago { get; set; }
        public string Concepto { get; set; }
        public int IdImputacion { get; set; }
        public int IdMoneda { get; set; }
        public int IdCompraIva { get; set; }
        public decimal Parcial { get; set; }
        public int Recibo { get; set; }
        public string NumeroPago { get; set; }
        [Display(Name = "Aplica Factura Nº ")]
        public int? IdCompraFacturaAplica { get; set; }
        public int IdDiario { get; set; }
        public int Auxiliar { get; set; }
        public string AxiliarNumero { get; set; }
        public bool Activo { get; set; }
        public int IdUsuario { get; set; }
        public DateTime UltimaModificacion { get; set; }

        public CompraIvaModelView CompraIva { get; set; }
        public ProveedorModelView Proveedor { get; set; }
        public ImputacionModelView Imputacion { get; set; }
        public CompraFacturaViewModel CompraFacturaAplicada { get; set; }

        public List<TipoMonedaModelView> TipoMonedas { get; set; }
        public List<TipoComprobanteModelView> TipoComprobante { get; set; }
    }
}