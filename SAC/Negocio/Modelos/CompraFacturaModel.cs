using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.ModeloDeDatos;
namespace Negocio.Modelos
{
    public class CompraFacturaModel
    {

        public int Id { get; set; }
        public int IdTipoComprobante { get; set; }
        public int PuntoVenta { get; set; }
        public int NumeroFactura { get; set; }
        public int IdProveedor { get; set; }
        public string IdTipoIva { get; set; }
        public string CAE { get; set; }
        public decimal Total { get; set; }
        public decimal Saldo { get; set; }
        public System.DateTime Fecha { get; set; }
        public System.DateTime Vencimiento { get; set; }
        public decimal TotalDolares { get; set; }
        public decimal Cotizacion { get; set; }
        public System.DateTime FechaPago { get; set; }
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
        public int IdCompraFacturaAplica { get; set; }
        public int IdDiario { get; set; }
        public int Auxiliar { get; set; }
        public string AxiliarNumero { get; set; }
        public Nullable<bool> Activo { get; set; }
        public int IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }
        
        public  CompraIvaModel CompraIva { get; set; }
        public  ImputacionModel Imputacion { get; set; }
        public  ProveedorModel Proveedor { get; set; }       
        public  List<CompraFacturaPago> CompraFacturaPago { get; set; }
     
       
    }

}
