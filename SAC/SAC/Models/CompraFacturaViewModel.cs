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

        public int IdTipoComprobante { get; set; }

        public int PuntoVenta { get; set; }

        public int NumeroFactura { get; set; }

        public int IdProveedor { get; set; }

        public string IdTipoIva { get; set; }

        public string CAE { get; set; }

        public Nullable<decimal> Total { get; set; }

        public Nullable<decimal> Saldo { get; set; }

        public System.DateTime Fecha { get; set; }

        public System.DateTime Vencimiento { get; set; }

        public Nullable<decimal> TotalDolares { get; set; }

        public decimal Cotizacion { get; set; }

        public Nullable<System.DateTime> FechaPago { get; set; }

        public Nullable<int> Periodo { get; set; }

        public Nullable<decimal> Grupo { get; set; }

        public string Marca { get; set; }

        public string Pase { get; set; }

        public Nullable<decimal> CotizacionDePago { get; set; }

        public string Concepto { get; set; }

        public Nullable<int> IdImputacion { get; set; }

        public Nullable<int> IdMoneda { get; set; }

        public Nullable<int> IdCompraIva { get; set; }

        public Nullable<decimal> Parcial { get; set; }

        public Nullable<int> Recibo { get; set; }

        public string NumeroPago { get; set; }

        public Nullable<int> IdCompraFacturaAplica { get; set; }

        public Nullable<int> Auxiliar { get; set; }

        public string AxiliarNumero { get; set; }

        public Nullable<bool> Activo { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        public Nullable<System.DateTime> UltimaModificacion { get; set; }

        public string tipoMonedaDescripcion { get; set; }


        public CompraIvaModelView CompraIva { get; set; }
        public ProveedorModelView Proveedor { get; set; }
        public TipoMonedaModelView TipoMoneda { get; set; }

        public List<TipoMonedaModelView> TipoMonedas { get; set; }
        public List<TipoComprobanteModelView> TipoComprobante { get; set; }

       




    }
}